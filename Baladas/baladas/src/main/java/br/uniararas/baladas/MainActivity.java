package br.uniararas.baladas;

import android.content.Intent;
import android.support.v7.app.ActionBarActivity;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;

public class MainActivity extends ActionBarActivity
{
    //Ciclo de Vida de uma Activity

    //private Button botaoMain;

    @Override
    protected void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        /*
        Intent intencao = getIntent();
        intencao.getExtras().putString("user", "fernando");
        intencao.getExtras().putString("pass", "123456");

        String user = intencao.getExtras().getString("user");
        String pass = intencao.getExtras().getString("pass");
        */

        //Intent intencao = new Intent(this, Atividade2.class);

        Intent intencao = new Intent(this, login.class);

        startActivity(intencao);
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu)
    {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.main, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item)
    {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();
        if (id == R.id.action_settings)
        {
            return true;
        }
        return super.onOptionsItemSelected(item);
    }

}
