package br.uniararas.baladas;

import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;

/**
 * Created by Fernando on 12/04/2014.
 */
public class Message
{
    public static void Show(Context context, String title, String message, Boolean positiveButton, Boolean negativeButton)
    {
        AlertDialog alert = null;
        AlertDialog.Builder builder = new AlertDialog.Builder(context);

        builder
            .setTitle(title)
            .setMessage(message)
            .setCancelable(false);

        if (positiveButton) {
            builder.setPositiveButton("OK", new DialogInterface.OnClickListener() {
                        @Override
                        public void onClick(DialogInterface dialogInterface, int i) {
                            dialogInterface.cancel();
                        }
                    }
            );
        }

        if (negativeButton)
        {
            //builder.setNegativeButton(null);
        }

        alert = builder.create();
        alert.show();
    }
}
