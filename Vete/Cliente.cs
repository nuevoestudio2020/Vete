using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data.SQLite;

namespace Vete
{
    public partial class Cliente : Form
    {
        public Cliente()
        {
            InitializeComponent();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private SQLiteDataAdapter DB;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();

        private void Cliente_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        //conexion a la bd
        private void SetConnection()
        {
            try
            {
                sql_con = new SQLiteConnection
                    ("Data Source = database.db; version = 3; New = False; Compress = True;");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        //ejecutar consulta
        private void ExecuteQuery(string txtQuery)
        {
            try
            {
                SetConnection();
                sql_con.Open();
                sql_cmd = sql_con.CreateCommand();
                sql_cmd.CommandText = txtQuery;
                sql_cmd.ExecuteNonQuery();
                sql_con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //cargar bd
        private void LoadData()
        {
            try
            {


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Seguro desea guardar registro ?", "Guardar", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string txtQuery = "update clientes Set Apellido = '" + txtApellido.Text + "', Direccion = '" + txtDireccion.Text + "', Telefono = '" + txtTelefono.Text + "', Mascota = '" + txtMascota.Text + "', Sexo = '" + txtSexo.Text + "', Nacimiento = '" + txtNacimiento.Text + "', Tamaño = '" + txtTamaño.Text + "', Vacunas = '" + txtVacunas.Text + "'  where Apellido = '" + txtApellido.Text + "' ";
                    ExecuteQuery(txtQuery);
                    LoadData();
                    MessageBox.Show("Registro actualizado correctamente !!");
                }
                else { }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Seguro desea agregar cliente ?", "Agregar", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string txtQuery = "insert into clientes (Apellido, Direccion, Telefono, Mascota, Sexo, Nacimiento, Tamaño, Vacunas) values('" + txtApellido.Text + "', '" + txtDireccion.Text + "', '" + txtTelefono.Text + "', '" + txtMascota.Text + "', '" + txtSexo.Text + "', '" + txtNacimiento.Text + "', '" + txtTamaño.Text + "', '" + txtVacunas.Text + "') ";
                    ExecuteQuery(txtQuery);
                    LoadData();
                    MessageBox.Show("Cliente agregado correctamente !!");
                }
                else { }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Seguro desea eliminar cliente ?", "Eliminar", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string txtQuery = "delete from clientes where Apellido = '" + txtApellido.Text + "' ";
                    ExecuteQuery(txtQuery);
                    LoadData();
                    MessageBox.Show("Cliente eliminado correctamente !!");
                }
                else { }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Seguro desea limpiar datos ?", "Limpiar datos", MessageBoxButtons.YesNo) == DialogResult.Yes) 
            {
                foreach (Control c in Controls)
                {
                    if (c is TextBox)
                    {
                        c.Text = "";
                    }
                }
               
            }
            else { }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Datos datos = new Datos();
            CertificadoVacunacion c = new CertificadoVacunacion();
            datos.Apelido = txtApellido.Text;
            datos.Direccion = txtDireccion.Text;
            datos.Telefono = txtTelefono.Text;
            datos.Mascota = txtMascota.Text;
            datos.Sexo = txtSexo.Text;
            datos.Nacimiento = txtNacimiento.Text;
            datos.Tamaño = txtTamaño.Text;
            datos.Vacunas = txtVacunas.Text;
            c.datos.Add(datos);
            c.Show();
        }
    }

    
}
