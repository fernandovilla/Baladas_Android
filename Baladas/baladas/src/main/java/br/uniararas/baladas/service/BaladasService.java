package br.uniararas.baladas.service;

import org.apache.http.HttpResponse;
import org.apache.http.NameValuePair;
import org.apache.http.client.HttpClient;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.message.BasicNameValuePair;

import java.io.IOException;
import java.io.InputStream;
import java.util.ArrayList;
import java.util.List;

import br.uniararas.baladas.model.Convert;
import br.uniararas.baladas.model.Usuario;

/**
 * Created by Fernando on 15/05/2014.
 */
public class BaladasService {

    public Usuario RealizarLogin(String username, String password)
    {
        Usuario usuario = null;
        String jsonResponse = "";

        try
        {
            //Begin the POST process...
            HttpBaladas.CreateHttpPOST(ServicePOST.REALIZAR_LOGIN);

            //Add parameters..
            HttpBaladas.AddPOSTParams("username", username);
            HttpBaladas.AddPOSTParams("password", password);

            //Execute POST method...
            HttpResponse response = HttpBaladas.ExecutePOST();

            if (response != null)
            {
                InputStream inputStream = response.getEntity().getContent();
                jsonResponse = Convert.InputStreamToString(inputStream);
                usuario = Usuario.Parse(jsonResponse);
            }

        } catch(IOException e){
            e.printStackTrace();
            usuario = null;
        }

        return usuario;
    }


}
