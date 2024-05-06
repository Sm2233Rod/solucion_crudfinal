using solucion_crud.logica;
using solucion_crud.modeloaddup;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace solucion_crud
{
    public partial class FormClientes : Form
    {
        public FormClientes()
        {
            InitializeComponent();
        }

        #region "mis variables"
        int nEstadoguarda = 0;
        string nom;
        string ape;
        int num;
        string ciuda;

        #endregion
        

        #region "mis metodos"
        private void Estado_texto(bool lEstado) 
        {
            textBox2.ReadOnly = !lEstado;
            textBox3.ReadOnly = !lEstado;
            textBox4.ReadOnly = !lEstado;
            textBox5.ReadOnly = !lEstado;
        }

        private void limpia_texto()
        {
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }

        private void Estado_botonupdate(bool lEstado)
        {
            saved.Visible = lEstado;
            cancel.Visible = lEstado;

            Buscar.Enabled = !lEstado;
            textBox1.Enabled = !lEstado;
        }

        private void Estado_btnprincipales(bool lEstado)
        {
            add.Enabled = lEstado;
            Update.Enabled = lEstado;
            Delete.Enabled = lEstado;
            Exit.Enabled = lEstado;
        }

        private void Selecciona_item_cliente()
        {

        }

       

        #endregion
        private void listadoclientes()
        {
            L_clientes Datos = new L_clientes();
            dataGridView1.DataSource = Datos.listado_clientes();
        }
 

        private void FormClientes_Load_1(object sender, EventArgs e)
        {
            this.listadoclientes();
        }

        private void add_Click(object sender, EventArgs e)
        {
            nEstadoguarda = 1; //nuevo registro
            this.Estado_texto(true);
            this.limpia_texto();
            this.Estado_botonupdate(true);
            this.Estado_btnprincipales(false);
            textBox2.Focus();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Estado_texto(false);
            this.limpia_texto();
            this.Estado_botonupdate(false);
            this.Estado_btnprincipales(true);
        }

        private void Update_Click(object sender, EventArgs e)
        {

           

            if (dataGridView1.SelectedRows.Count <= 0)
            {
                MessageBox.Show("no se tiene registro para actualizar",
                    "aviso del sistema",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            else
            {

                // Obtener los datos del cliente seleccionado en el DataGridView
                string nombrecliente = dataGridView1.SelectedRows[0].Cells["nombrecliente"].Value.ToString();
                string apellidocliente = dataGridView1.SelectedRows[0].Cells["apellidocliente"].Value.ToString();

                // Obtener el ID del cliente
                L_clientes datosClientes = new L_clientes();
                int idcliente = datosClientes.ObtenerIdCliente(nombrecliente, apellidocliente);

                if (idcliente != -1)
                {

                    Selecciona_item_cliente();

                    // Actualizar al cliente con el ID obtenido
                    nEstadoguarda = 2; // Indicar actualización de registro
                    textBox2.Text = nombrecliente;
                    textBox3.Text = apellidocliente;

                    this.limpia_texto();
                    this.Estado_botonupdate(true);
                    this.Estado_btnprincipales(false);
                    this.Estado_texto(true);
                    textBox2.Focus();

                }
                else
                {
                    MessageBox.Show("El cliente seleccionado no pudo ser encontrado.",
                                    "Aviso del sistema",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
                
            }
        }
       
        

        private void saved_Click(object sender, EventArgs e)
        {
            string Rpta = "";
            R_clientes oCi = new R_clientes();

            oCi.nombrecliente = nom;
            oCi.nombrecliente = textBox2.Text.Trim();
            oCi.apellidocliente = ape;
            oCi.apellidocliente = textBox3.Text.Trim();

            if (int.TryParse(textBox4.Text.Trim(), out int telefono))
            {
                oCi.telefono = telefono; // Asigna el valor de teléfono si la conversión es exitosa
            }
            else
            {
                MessageBox.Show("Por favor, ingrese un número de teléfono válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Sale del método si la conversión falla
            }

            oCi.ciudad = ciuda;
            oCi.ciudad = textBox5.Text.Trim();

            L_clientes Datos = new L_clientes();
            Rpta = Datos.Guardar_clientes(nEstadoguarda,oCi);

            if (Rpta.Equals("ok"))
            {
                this.listadoclientes();
                MessageBox.Show("los datos han sido guardados exitosamente",
                    "aviso del sistema",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(Rpta);
            }

            if (nEstadoguarda==2)
            {

            }

            this.Estado_texto(false);
            this.limpia_texto();
            this.Estado_botonupdate(false);
            this.Estado_btnprincipales(true);



        }

        private void Buscar_Click(object sender, EventArgs e)
        {
            this.listadoclientes(textBox1.Text.Trim());
        }
    }
}

    