﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lavanderia.util
{
    class Validacion
    {
        public void soloLetras(KeyPressEventArgs e) {
            try
            {
                if (Char.IsLetter(e.KeyChar))
                {
                    e.Handled = false;      
                }
                else if (Char.IsControl(e.KeyChar)) {
                    e.Handled = false;

                }
                else if (Char.IsSeparator(e.KeyChar))
                {
                    e.Handled = false;
                }
                else {
                    e.Handled = true;
                }
            }
            catch (Exception) { 
            
            
            }


        }

        public void soloNumeros(KeyPressEventArgs e)
        {
            try
            {
                if (Char.IsNumber(e.KeyChar))
                {
                    e.Handled = false;
                }
                else if (Char.IsControl(e.KeyChar))
                {
                    e.Handled = false;

                }
                else if (Char.IsSeparator(e.KeyChar))
                {
                    e.Handled = false;
                }
                else if (Char.IsPunctuation(e.KeyChar))
                {
                    e.Handled = false;
                }

                else
                {
                    e.Handled = true;
                }
            }
            catch (Exception)
            {


            }


        }
    }
}
