package br.uniararas.baladas.model;

import android.app.ProgressDialog;
import android.content.Context;
import android.os.AsyncTask;

import java.io.IOException;

import br.uniararas.baladas.service.BaladasService;

/**
 * Created by Fernando on 15/05/2014.
 */
public class RealizarLogin extends AsyncTask<Usuario, Integer, Usuario> {

    private Context context;
    private ProgressDialog progress = null;
    private String username;
    private String password;

    @Override
    protected Usuario doInBackground(Usuario... usuarios) {

        Usuario usuario = null;

        try {
            BaladasService service = new BaladasService();
            usuario = service.RealizarLogin(this.username, this.password);
            publishProgress(20);

        }catch(Exception e)
        {
            usuario = null;
            e.printStackTrace();
        }

        return usuario;
    }

    @Override
    protected void onPreExecute() {
        super.onPreExecute();

        this.progress.show();
    }

    @Override
    protected void onPostExecute(Usuario usuario) {
        super.onPostExecute(usuario);

        this.progress.dismiss();
    }

    @Override
    protected void onProgressUpdate(Integer... values) {
        super.onProgressUpdate(values);
    }

    @Override
    protected void onCancelled() {
        super.onCancelled();

        this.progress.dismiss();
    }

    public RealizarLogin(Context context, String username, String password)
    {
        this.context = context;
        this.progress = new ProgressDialog(context);
        this.progress.setTitle("Baladas");
        this.progress.setMessage("Aguarde...");

        this.username = username;
        this.password = password;
    }
}
