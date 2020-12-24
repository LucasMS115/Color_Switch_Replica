using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Runtime.InteropServices;

public class Atividade : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void comunicarCodigoAtividade(string valor);
    [DllImport("__Internal")]
    private static extern void comunicarFimAtividade();

    public string nomeJogo = "Jogo das Cores";
    public string catogoriaJogo = "padrao";
    public string atividadeCodigo;

    public Acesso acesso;

    void Start()
    {
        acesso = GetComponent<Acesso>();     
    }

        public void criarAtividade()
    {

        StartCoroutine(criarAtividade(acesso.url + "activity/new/?name="+nomeJogo+"&category="+catogoriaJogo));

    }

    public void encerrarAtividade()
    {

        StartCoroutine(encerrarAtividade(acesso.url + "activity/" + atividadeCodigo + "/end"));

    }

    IEnumerator criarAtividade(string urlDados)
    {
        UnityWebRequest www = UnityWebRequest.Get(urlDados);
        www.SetRequestHeader("Authorization", "Bearer " + acesso.token);
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            atividadeCodigo = www.downloadHandler.text;
            Debug.Log("atividadeCodigo => " + atividadeCodigo);
            comunicarCodigoAtividade(atividadeCodigo);
            yield return new WaitForSeconds(5);
            encerrarAtividade();
        }
    }


    IEnumerator encerrarAtividade(string urlDados)
    {
        UnityWebRequest www = UnityWebRequest.Get(urlDados);
        www.SetRequestHeader("Authorization", "Bearer " + acesso.token);
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            string jsonString = www.downloadHandler.text;
            comunicarFimAtividade();
            print(jsonString);
        }
    }
}
