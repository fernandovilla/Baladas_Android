package br.uniararas.baladas.service;

import org.apache.http.Header;
import org.apache.http.HeaderIterator;
import org.apache.http.HttpEntity;
import org.apache.http.HttpResponse;
import org.apache.http.NameValuePair;
import org.apache.http.ProtocolVersion;
import org.apache.http.StatusLine;
import org.apache.http.client.ClientProtocolException;
import org.apache.http.client.HttpClient;
import org.apache.http.client.entity.UrlEncodedFormEntity;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.message.BasicNameValuePair;
import org.apache.http.params.BasicHttpParams;
import org.apache.http.params.HttpParams;

import java.io.IOException;
import java.io.InputStream;
import java.util.ArrayList;
import java.util.List;
import java.util.Locale;

import br.uniararas.baladas.model.Convert;
import br.uniararas.baladas.model.Usuario;

/**
 * Created by Fernando on 15/05/2014.
 */
public class HttpBaladas {

    private static final String URL_REALIZAR_LOGIN = "http://fernandotestews.somee.com/Baladas/BaladasService.asmx?op=ListarBaladas";
    private static final String URL_SALVAR_BALADA = "http://fernandotestews.somee.com/Baladas/BaladasService.asmx?op=SalvarBalada";
    private static final String URL_LISTAR_BALADAS = "http://fernandotestews.somee.com/Baladas/BaladasService.asmx?op=ListarBaladas";
    private static final String URL_LISTAR_PRESENCAS = "http://fernandotestews.somee.com/Baladas/BaladasService.asmx?op=ListarPresencas";
    private static final String URL_CONFIRMAR_PRESENCA = "http://fernandotestews.somee.com/Baladas/BaladasService.asmx?op=ConfirmarPresenca";
    private static final String URL_CADASTRAR_USUARIO = "http://fernandotestews.somee.com/Baladas/BaladasService.asmx?op=CadastrarUsuario";

    private static HttpGet httpGet = null;
    private static HttpPost httpPost = null;
    private static HttpParams getParams = null;
    private static HttpParams postParams = null;

    public static HttpGet CreateHttpGet(ServiceGET serviceType)
    {
        httpGet = null;

        switch (serviceType)
        {
            case LISTAR_BALADAS:
                httpGet = new HttpGet(URL_LISTAR_BALADAS);
                break;
            case LISTAR_PRESENCAS:
                httpGet = new HttpGet(URL_LISTAR_PRESENCAS);
                break;
            default:
                httpGet = null;
        }

        return httpGet;
    }

    public static HttpPost CreateHttpPOST(ServicePOST serviceType)
    {
        httpPost = null;

        switch (serviceType)
        {
            case REALIZAR_LOGIN:
                httpPost = new HttpPost(URL_REALIZAR_LOGIN);
                break;
            case CADASTRAR_USUARIO:
                httpPost = new HttpPost(URL_CADASTRAR_USUARIO);
                break;
            case SALVAR_BALADA:
                httpPost = new HttpPost(URL_SALVAR_BALADA);
                break;
            case CONFIRMAR_PRESENCA:
                httpPost = new HttpPost(URL_CONFIRMAR_PRESENCA);
                break;
            default:
                httpPost = null;
        }

        if (httpPost != null){
            httpPost.setHeader("Content-type","application/json");
            httpPost.setHeader("Accept","application/json");
        }

        return httpPost;
    }

    public static void AddGETParams(String nameParam, Object valueParam)
    {
        //if (getParams == null) { getParams = new ArrayList<NameValuePair>(); }
        //getParams.add(valuePair);

        if (httpGet != null)
        {
            if (getParams == null) { getParams = new BasicHttpParams(); }
            getParams.setParameter(nameParam, valueParam);
        }
    }

    public static void AddPOSTParams(String nameParam, Object valueParam)
    {
        //if (postParams == null) { postParams = new ArrayList<NameValuePair>(); }
        //postParams.add(valuePair);

        if (httpPost != null)
        {
            if (postParams == null) { postParams = new BasicHttpParams(); }
            postParams.setParameter(nameParam, valueParam);
        }
    }

    public static HttpResponse ExecuteGET()
    {
        if (httpGet == null) { return null; }

        HttpResponse response = null;

        try {

            if (getParams != null) {
                httpGet.setParams(getParams);
            }

            HttpClient client = new DefaultHttpClient();
            response = client.execute(httpGet);

        }catch(ClientProtocolException e){
            e.printStackTrace();
            response = null;
        }catch(IOException e){
            e.printStackTrace();
            response = null;
        }

        return response;
    }

    public static HttpResponse ExecutePOST()
    {
        if (httpPost == null) { return null; }

        HttpResponse response = null;

        try {
            if (postParams != null) {
                //UrlEncodedFormEntity entity = new UrlEncodedFormEntity(postParams, "UTF-8");
                //httpPost.setEntity(entity);

                httpPost.setParams(postParams);
            }

            HttpClient client = new DefaultHttpClient();
            response = client.execute(httpPost);

        }catch(IOException e){
            e.printStackTrace();
            response = null;
        }

        return response;
    }

    public static void Clear()
    {
        httpGet = null;
        httpPost = null;
        getParams = null;
        postParams = null;
    }
}
