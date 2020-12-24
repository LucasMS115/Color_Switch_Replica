using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Networking;

public class Acesso : MonoBehaviour
{
    public string token; 

    public string url = "https://personid.renan.cc/api/";
    public Usuario usuarioLogado;

    public void Start(){
       /*  token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJhdWQiOiI5MjQ2ZjI4My00NjE4LTQwZDItODE1OC0yYzE1OTM3ODQ5NDEiLCJqdGkiOiJhODgxMTdmZmI1ZmFkMGNmYjdjYzk5Y2NiMDM4MDAzZjg2MTFmMmI1ZjEyYmU2ZDZmNWI5ZjI5MGQ5MWI2MGMzZTY0ZGQwNzcyZGQ5MGQxNSIsImlhdCI6MTYwODczNTQ4MCwibmJmIjoxNjA4NzM1NDgwLCJleHAiOjE2NDAyNzE0ODAsInN1YiI6IjE1Iiwic2NvcGVzIjpbXX0.FHyDAzEH8dnjV_SaoZttJkwQ5wt7Qj6ng0xHVSxOA-2vyc6QrBswcRO9sc_GeXb6WGptF6tZvmPZphhggEf3eLn1_Wh6je2pWJfoL5tQsWW7lwddfBp9mxjgmy7O2elvNZTUUF9PWXcC7iVk3qB-kdVbdxaw4QYd2qEayWZfCWOAtL9StQF6GFh1Y-XdHEoVnD7HTye5T8WqfM_yFcK505pr5AqIJB2AqGO67vQzlsUxRk-kWXCjNZMvHCG-_NulmqgNI8iyAVJPUW82QPly1rh3kVd7IxxjkNHbxTTBRQeKdyOFCfkJpf0DOvJpTIfyLPsf8_J-Nu97DubGtnXIh_foEN0N4xUwvAhH93d4Kn316T8R6hNeZ2OSv8ASTfdSQvRiiS-Fxl1bfvanXci4OAz0AmtD7T9urNOCON8RQQ_TrTaF2E9sX9mbsVmtVgizrTfd9epQI-Rk3MmER6wZjb0aLp2Zf4M_2QFnBKQEnfVrfUBX8vah4W-LEpjR9KwRMIoNULm01BS6OFfzZde4ipgLCMUr83nO2PGwWrqF_NyuXiL-TWOOmnBOzwGDy05moZKD06q_fT5toSAxUjb4bSec_oMtZtRpJM7DScd3l21Gv6M2Ilys3Pp_YFEV2uLmWXwl9yoLKVVWO3Tl9DRk2eQTiidF7f6NnADxUUxSl18"; */
    }

    public void receberToken(string t)
    {
        //**
        
        token = t;
        StartCoroutine(obterDadosUsuario());
    }

    IEnumerator obterDadosUsuario()
    {
        //**

        UnityWebRequest www = UnityWebRequest.Get(url + "user");
        www.SetRequestHeader("Authorization", "Bearer " + token);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            string jsonString = www.downloadHandler.text;
            usuarioLogado = JsonConvert.DeserializeObject<Usuario>(jsonString);
        }
    }
}


public class Usuario : MonoBehaviour
{
    public string id;
    public string name;
}