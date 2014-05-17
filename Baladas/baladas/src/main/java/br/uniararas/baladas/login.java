package br.uniararas.baladas;

import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.support.v7.app.ActionBarActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import br.uniararas.baladas.model.RealizarLogin;
import br.uniararas.baladas.model.Usuario;

public class login extends ActionBarActivity
{
    private Button btnEnter = null;
    private Button btnNewAcount = null;
    private EditText edtUserName = null;
    private EditText edtPassword = null;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        this.InitializeComponents();
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.login, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();
        if (id == R.id.action_settings) {
            return true;
        }
        return super.onOptionsItemSelected(item);
    }

    private void InitializeComponents()
    {
        edtUserName = (EditText)findViewById(R.id.login_et_userName);
        edtPassword = (EditText)findViewById(R.id.login_et_password);
        btnEnter = (Button)findViewById(R.id.login_bt_Enter);
        btnNewAcount = (Button)findViewById(R.id.login_bt_NewAcount);

        btnEnter.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) { btnEnter_OnClick(view); }
        });

        btnNewAcount.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) { btnNewAcount_OnClick(view); }
        });
    }

    private void btnEnter_OnClick(View view)
    {
        String username = edtUserName.getText().toString();
        String password = edtPassword.getText().toString();

        RealizarLogin realizarLogin = new RealizarLogin(this, username, password);

        realizarLogin.execute(new Usuario());

        Log.i("UserName: " + this.edtUserName.getText().toString(), "Password: " + this.edtPassword.getText().toString());

        //if (loginOK)
        //{
        //    this.ShowPesquisaActivity();
        //}
        //else
        //{
        //    Message.Show(this, "Acesso Negado", "Login ou senha são inválidos", true, false);
        //    //Toast.makeText(login.this, "Acesso Negado!", Toast.LENGTH_LONG);
        //}
    }

    private void btnNewAcount_OnClick(View view)
    {
        ShowNewAcount();
    }

    private void ShowPesquisaActivity()
    {
        try
        {
            Intent intencao = new Intent(login.this, pesquisa.class);

            intencao.putExtra("UserName", edtUserName.getText().toString());
            intencao.putExtra("Password", edtPassword.getText().toString());

            startActivity(intencao);
        }
        catch(Exception ex)
        {
            Log.i("Deu ERRO!", ex.getMessage());
        }
    }

    private void ShowNewAcount()
    {

    }
}
