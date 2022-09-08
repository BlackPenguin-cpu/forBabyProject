using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;


/// <summary>
/// ������ƮǮ ���� ������Ʈ�� ���� �������̽��̴�
/// </summary>
public interface IObjectPoolingObj
{
    /// <summary>
    /// �� �Լ��� ������Ʈ Ǯ�� ó�� ���������� �ߵ��ȴ�
    /// </summary>
    void OnObjCreate();
}

public class ObjectPoolClass
{
    public GameObject parentObj;
    public Queue<GameObject> objNotActiveQueue = new Queue<GameObject>();
    public Queue<GameObject> objActiveQueue = new Queue<GameObject>();
}

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;
    private Dictionary<string, ObjectPoolClass> ParentObj = new Dictionary<string, ObjectPoolClass>();
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void OnObjCreate(GameObject obj)
    {

        if (obj.TryGetComponent(out IObjectPoolingObj poolingObj))
        {
            poolingObj.OnObjCreate();
        }
        else
        {
            Debug.Log("������ƮǮ ���� �߻� (������ƮǮ �������̽� ��� ����)");
        }
    }
    public GameObject CreateObj(GameObject obj, bool isNotStartFunc = false)
    {
        if (ParentObj.ContainsKey(obj.name) == false)
        {
            GameObject newObj = new GameObject(obj.name + "_ParentObj");
            newObj.transform.parent = gameObject.transform;
            ParentObj[obj.name] = new ObjectPoolClass() { parentObj = newObj, objNotActiveQueue = new Queue<GameObject>(), objActiveQueue = new Queue<GameObject>() };
            ParentObj[obj.name].parentObj = newObj;

            return CreateObj(obj);
            //return Instantiate(obj, ParentObj[obj.name].parentObj.transform);
        }
        else
        {
            GameObject returnObj;
            if (ParentObj[obj.name].objNotActiveQueue.Count > 0)
            {
                returnObj = ParentObj[obj.name].objNotActiveQueue.Dequeue();
                returnObj.SetActive(true);
            }
            else
            {
                returnObj = Instantiate(obj, ParentObj[obj.name].parentObj.transform);
            }
            ParentObj[obj.name].objActiveQueue.Enqueue(returnObj);
            if (!isNotStartFunc)
                OnObjCreate(returnObj);
            return returnObj;
        }
    }
    public GameObject CreateObj(GameObject obj, Vector3 pos, Quaternion quaternion)
    {
        GameObject returnObj = CreateObj(obj, true);
        returnObj.transform.position = pos;
        returnObj.transform.rotation = quaternion;
        OnObjCreate(returnObj);

        return returnObj;
    }
    public void DeleteObj(GameObject obj)
    {
        if (ParentObj.ContainsKey(textCloneRemove(obj.name)))
        {
            ParentObj[textCloneRemove(obj.name)].objNotActiveQueue.Enqueue(obj);
            obj.gameObject.SetActive(false);
            obj.transform.position = Vector3.zero;
        }
        else
        {
            Debug.Log("������ƮǮ ���� �߻� (�� ��ü�� ������ƮǮ ��ü�� �ƴմϴ�)");
            Destroy(obj);
        }
    }
    public GameObject[] ReturnObjList(GameObject obj, bool getActive = true)
    {
        if (getActive)
        {
            return ParentObj[textCloneRemove(obj.name)].objActiveQueue.ToArray();
        }
        else
        {
            return ParentObj[textCloneRemove(obj.name)].objNotActiveQueue.ToArray();
        }
    }
    string textCloneRemove(string objName)
    {
        StringBuilder builder = new StringBuilder(objName);
        builder.Replace("(Clone)", "");
        return builder.ToString();
    }

    private void OnLevelWasLoaded(int level)
    {
        foreach (Transform obj in transform.GetComponentInChildren<Transform>())
        {
            Destroy(obj);
        }
    }
}