using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;
using UnityEngine.Networking;


public class Log : MonoBehaviour
{
    public List<Registro> logs = new List<Registro>();
    private int tamanhoBloco = 5;
    public DateTime dt;
    public Acesso acesso;
    public Atividade atividade;

    void Start()
    {
        atividade = GetComponent<Atividade>();
        acesso = GetComponent<Acesso>();
        Debug.Log("Start log token => " + acesso.token);
        Debug.Log("Start log url => " + acesso.url);
        Debug.Log("Start log atividadeCodigo => " + atividade.atividadeCodigo);


        StartCoroutine(horario());

    }

    // Update is called once per frame
    void Update()
    {
        if (logs.Count == tamanhoBloco) {
            string json = JsonConvert.SerializeObject(logs);
            Debug.Log(json);
            StartCoroutine(salvarLog(json));
            logs.Clear();        
            
        }
    }

    public void addLog(string name, string value, string category) {
          logs.Add(new Registro(name,value,category, dt.ToString("yyyy-M-d H:mm:ss")));
    }


    IEnumerator salvarLog(string dados)
    {
        WWWForm form = new WWWForm();
        form.AddField("logs", dados);

        using (UnityWebRequest www = UnityWebRequest.Post(acesso.url + "activity/"+ atividade.atividadeCodigo + "/log", form))
        {
            www.SetRequestHeader("Authorization", "Bearer " + acesso.token);
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }

    /* Date */

    IEnumerator horario()
    {
        UnityWebRequest www = UnityWebRequest.Get(acesso.url + "time");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            dt = DateTime.Parse(www.downloadHandler.text);
            StartCoroutine(UpdateServerTime());
        }
    }

    IEnumerator UpdateServerTime()
    {
        while (true)
        {
            dt = dt.AddSeconds(1);
            yield return new WaitForSeconds(1);
        }
    }
}

public class Registro {
    public string name;
    public string value;
    public string category;
    public string time;

    public Registro(string _name, string _value, string _category, string _time)
    {
        name = _name;
        value = _value;
        category = _category;
        time = _time;
    }
}
