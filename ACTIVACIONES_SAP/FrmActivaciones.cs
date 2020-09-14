using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MRS
{
    public partial class FrmActivaciones : Form
    {



        #region CONSTRUCTOR
        public FrmActivaciones()
        {
            InitializeComponent();
        }



        #endregion

        #region VARIABLES

        public static List<Listas.ListaSocioNegocio> DatosSN = new List<Listas.ListaSocioNegocio>();
        public static List<Listas.ListaMunicipios> DatosMunicipios = new List<Listas.ListaMunicipios>();
        public static List<Listas.ListaColonias> DatosColonias = new List<Listas.ListaColonias>();
        private static Conexiones.ConexionSAP _oConnection = new Conexiones.ConexionSAP();
        private SAPbobsCOM.Company oCompany;

        #endregion

        #region METODOS

        private void FrmActivaciones_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Extensor.CargarConfiguraciones())
                {
                    Application.Exit();
                }
                else
                {
                    ObtenerDatosConfig();
                    CargarMunicipios();
                    grpDatosAfiliado.Enabled = false;
                    //ConectarAddon();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Activaciones: " + ex.Message, "Activaciones SAP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ObtenerDatosConfig()
        {
            try
            {
                lblSucursal.Text = Extensor.Configuracion.VENTANA.Sucursal;
                //this.BackColor = Color.FromName(Extensor.Configuracion.VENTANA.Color);
                //grpDatosAfiliado.BackColor = Color.FromName(Extensor.Configuracion.VENTANA.Color);
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ObtenerDatosConfig: " + ex.Message);
            }
        }

        private void CargarMunicipios()
        {
            try
            {
                DatosMunicipios = Extensor.ObtenerMunicipios();
                cmbMunicipio.DataSource = DatosMunicipios;
                cmbMunicipio.ValueMember = "Municipio";
                cmbMunicipio.DisplayMember = "Municipio";
                cmbMunicipio.SelectedValue = "";
            }
            catch (Exception ex)
            {
                throw new Exception("Error en ObtenerDatosConfig: " + ex.Message);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
            salirAddon();
        }

        private void txtNumeroSolicitud_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if ((int)e.KeyChar == (int)Keys.Enter)
                {
                    if (!string.IsNullOrEmpty(txtNumeroSolicitud.Text))
                    {
                        if (Extensor.CodigoActivacion(txtNumeroSolicitud.Text))
                        {
                            limpiarControles();
                            DatosSN = Extensor.ObtenerDatosSN(txtNumeroSolicitud.Text);
                            txtCodigoAsistente.Text = DatosSN.ElementAt(0).codigoAsistente;
                            txtNombreAsistente.Text = DatosSN.ElementAt(0).NombreAsistente;
                            dtpFechaNacimiento.Text = DatosSN.ElementAt(0).FechaNacimiento.ToString("dd/MM/yyyy");
                            txtTelefono.Text = DatosSN.ElementAt(0).Telefono;
                            txtNombre.Text = DatosSN.ElementAt(0).NombreSN;
                            txtDireccion.Text = DatosSN.ElementAt(0).Direccion;
                            txtEntreCalles.Text = DatosSN.ElementAt(0).EntreCalles;
                            cmbMunicipio.Text = DatosSN.ElementAt(0).Municipio;
                            cmbColonias.DataSource = null;
                            DatosColonias = Extensor.ObtenerColonias(cmbMunicipio.SelectedValue.ToString());
                            cmbColonias.DataSource = DatosColonias;
                            cmbColonias.ValueMember = "Colonia";
                            cmbColonias.DisplayMember = "Colonia";
                            cmbColonias.SelectedValue = DatosSN.ElementAt(0).Colonia;
                            txtObservaciones.Text = DatosSN.ElementAt(0).Observaciones;
                            txtNvoIngreso.Text = DatosSN.ElementAt(0).NvoIngreso;
                            txtCodigoPostal.Text = DatosSN.ElementAt(0).CodigoPostal;
                            txtRfc.Text = DatosSN.ElementAt(0).RFC;
                            txtCodigoActivacion.Text = DatosSN.ElementAt(0).CodigoActivacion;
                            txtAgenteCapturo.Text = DatosSN.ElementAt(0).QuienCaptura;
                            cmbEsquema.SelectedItem = DatosSN.ElementAt(0).Esquema;
                            grpDatosAfiliado.Enabled = false;
                        }
                        else
                        {
                            DatosSN.Clear();
                            DatosSN = Extensor.ObtenerDatosSolicitud(txtNumeroSolicitud.Text);
                            if (DatosSN.Count() > 0)
                            {
                                limpiarControles();
                                txtCodigoAsistente.Text = DatosSN.ElementAt(0).codigoAsistente;
                                txtNombreAsistente.Text = DatosSN.ElementAt(0).NombreAsistente;
                                string esquema = DatosSN.ElementAt(0).Esquema;

                                if (esquema == "COMISION")
                                {
                                    cmbEsquema.SelectedItem = esquema;
                                    cmbEsquema.Enabled = false;
                                }
                                else
                                {
                                    cmbEsquema.SelectedItem = "";
                                    cmbEsquema.Enabled = true;
                                }

                                grpDatosAfiliado.Enabled = true;
                                txtNombre.Focus();
                            }
                            else
                            {
                                MessageBox.Show("No existe la solicitud", "Obtener datos Solicitud", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                limpiarControles();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Capturar la solicitud", "Solicitud", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        limpiarControles();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al validar solicitud: " + ex.Message, "Validar solicitud", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void limpiarControles()
        {
            try
            {
                cmbColonias.DataSource = null;
                txtCodigoAsistente.Clear();
                txtNombreAsistente.Clear();
                dtpFechaNacimiento.Text = "";
                txtTelefono.Clear();
                txtNombre.Clear();
                txtApellidoM.Clear();
                txtApellidoP.Clear();
                txtDireccion.Clear();
                txtEntreCalles.Clear();
                cmbMunicipio.SelectedValue = "";
                cmbColonias.Text = "";
                txtObservaciones.Clear();
                txtNvoIngreso.Clear();
                txtCodigoPostal.Clear();
                txtRfc.Clear();
                txtCodigoActivacion.Clear();
                txtAgenteCapturo.Clear();
                cmbEsquema.SelectedItem = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al limpiar controles: " + ex.Message, "Limpiar controles", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cmbMunicipio_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                //    if (cmbColonias.Items.Count > 0)
                //        cmbColonias.Items.Clear();
                cmbColonias.DataSource = null;
                DatosColonias = Extensor.ObtenerColonias(cmbMunicipio.SelectedValue.ToString());
                cmbColonias.DataSource = DatosColonias;
                cmbColonias.ValueMember = "Colonia";
                cmbColonias.DisplayMember = "Colonia";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener colonias: " + ex.Message, "Colonias", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConectarAddon()
        {
            try
            {
                string msgError = null;
                _oConnection = new Conexiones.ConexionSAP();
                if (!_oConnection.ConectarSAP(ref msgError))
                {
                    MessageBox.Show(msgError);
                    System.Environment.Exit(0);
                }
                else
                {
                    oCompany = _oConnection._oCompany;
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }

        public void salirAddon()
        {
            try
            {
                if (oCompany.Connected == true)
                {
                    oCompany.Disconnect();
                }

                System.Windows.Forms.Application.Exit();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.Application.Exit();
            }
        }

        //private void txtApellidoM_Leave(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        var rfc = "";
        //        if (!string.IsNullOrEmpty(txtNombre.Text))
        //        {
        //            if (!string.IsNullOrEmpty(txtApellidoP.Text))
        //            {
        //                if (!string.IsNullOrEmpty(txtApellidoM.Text))
        //                {
        //                    rfc = RfcFacil.RfcBuilder.ForNaturalPerson()
        //                                           .WithName(txtNombre.Text)
        //                                           .WithFirstLastName(txtApellidoP.Text)
        //                                           .WithSecondLastName(txtApellidoM.Text)
        //                                           .WithDate(Convert.ToInt32(dtpFechaNacimiento.Value.Year), Convert.ToInt32(dtpFechaNacimiento.Value.Month), Convert.ToInt32(dtpFechaNacimiento.Value.Day))
        //                                           .Build().ToString();

        //                    txtRfc.Text = rfc.ToString();
        //                }
        //                else
        //                {
        //                    MessageBox.Show("Capture el apellido materno");
        //                }
        //            }
        //            else
        //            {
        //                MessageBox.Show("Capture el apellido paterno");
        //            }
        //        }
        //        else
        //        {
        //            MessageBox.Show("Capture el nombre");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error al generar RFC: " + ex.Message);
        //    }
        //}

        private void txtCodigoPostal_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsNumber(e.KeyChar) && e.KeyChar != Convert.ToChar(Keys.Back);
        }

        private void btnCodigoActivacion_Click(object sender, EventArgs e)
        {
            try
            {
                string msgError = ValidarDatos();
                string CodigoActivacion = null;
                string esquemaValidacion = null;

               
                if (string.IsNullOrEmpty(msgError))
                {
                    if (cmbEsquema.Text == "SUELDO")
                    {
                        esquemaValidacion = Extensor.ValidarEsquemaPago(cmbEsquema.Text, txtCodigoAsistente.Text);
                        cmbEsquema.SelectedItem = esquemaValidacion;
                    }
                    else
                    {
                        esquemaValidacion = "COMISION";
                        cmbEsquema.SelectedItem = esquemaValidacion;
                    }

                    Extensor.GenearLeadSAP(txtAgente.Text, txtNumeroSolicitud.Text, txtCodigoAsistente.Text, txtNombreAsistente.Text,
                       dtpFechaNacimiento.Value.Day, dtpFechaNacimiento.Value.Month, dtpFechaNacimiento.Value.Year, txtTelefono.Text, txtNombre.Text, txtApellidoP.Text,
                       txtApellidoM.Text, txtDireccion.Text, txtEntreCalles.Text, cmbMunicipio.Text, cmbColonias.Text, txtObservaciones.Text, txtNvoIngreso.Text,
                       txtCodigoPostal.Text, txtRfc.Text, esquemaValidacion, ref msgError, ref CodigoActivacion);

                    if (!msgError.Contains("Error"))
                    {
                        txtCodigoActivacion.Text = CodigoActivacion;
                        MessageBox.Show(msgError, "Código activación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        grpDatosAfiliado.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show(msgError, "Generar activación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show(msgError, "Datos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar código de activación: " + ex.Message, "Código activación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private string ValidarDatos()
        {
            string msgError = null;
            try
            {
                if (string.IsNullOrEmpty(txtAgente.Text))
                    return msgError = "Capturar nombre del agente";
                if (string.IsNullOrEmpty(txtTelefono.Text))
                    return msgError = "Capturar teléfono";
                if (string.IsNullOrEmpty(txtNombre.Text))
                    return msgError = "Capturar nombre del afiliado";
                if (string.IsNullOrEmpty(txtApellidoP.Text))
                    return msgError = "Capturar apellido paterno";
                if (string.IsNullOrEmpty(txtApellidoM.Text))
                    return msgError = "Capturar apellido materno";
                if (string.IsNullOrEmpty(txtDireccion.Text))
                    return msgError = "Capturar calle / número";
                if (string.IsNullOrEmpty(cmbMunicipio.Text))
                    return msgError = "Seleccionar municipio";
                if (string.IsNullOrEmpty(cmbColonias.Text))
                    return msgError = "Seleccionar colonia";
                if (string.IsNullOrEmpty(txtRfc.Text))
                    return msgError = "No se generó el RFC";
                if (string.IsNullOrEmpty(cmbEsquema.Text))
                    return msgError = "Seleccionar esquema de pago";
                return msgError;
            }
            catch (Exception ex)
            {
                msgError = ex.Message;
                return msgError;
            }
        }

        private void btnUpdateMunicipios_Click(object sender, EventArgs e)
        {
            try
            {
                cmbMunicipio.DataSource = null;
                CargarMunicipios();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar municipios: " + ex.Message, "Actualizar municipios", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmActivaciones_FormClosed(object sender, FormClosedEventArgs e)
        {
            salirAddon();
        }


        #endregion

        private void txtTelefono_Leave(object sender, EventArgs e)
        {
            try
            {
                var rfc = "";
                if (!string.IsNullOrEmpty(txtNombre.Text))
                {
                    if (!string.IsNullOrEmpty(txtApellidoP.Text))
                    {
                        if (!string.IsNullOrEmpty(txtApellidoM.Text))
                        {
                            rfc = RfcFacil.RfcBuilder.ForNaturalPerson()
                                                   .WithName(txtNombre.Text)
                                                   .WithFirstLastName(txtApellidoP.Text)
                                                   .WithSecondLastName(txtApellidoM.Text)
                                                   .WithDate(Convert.ToInt32(dtpFechaNacimiento.Value.Year), Convert.ToInt32(dtpFechaNacimiento.Value.Month), Convert.ToInt32(dtpFechaNacimiento.Value.Day))
                                                   .Build().ToString();

                            txtRfc.Text = rfc.ToString();
                        }
                        else
                        {
                            MessageBox.Show("Capture el apellido materno");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Capture el apellido paterno");
                    }
                }
                else
                {
                    MessageBox.Show("Capture el nombre");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar RFC: " + ex.Message);
            }
        }

        private void txtNumeroSolicitud_Click(object sender, EventArgs e)
        {
            txtNumeroSolicitud.SelectAll();
        }

        private void txtCodigoActivacion_Click(object sender, EventArgs e)
        {
            txtCodigoActivacion.SelectAll();
        }



    }
}
