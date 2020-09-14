namespace MRS
{
    partial class FrmActivaciones
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmActivaciones));
            this.lblSucursal = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNumeroSolicitud = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCodigoAsistente = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNombreAsistente = new System.Windows.Forms.TextBox();
            this.grpDatosAfiliado = new System.Windows.Forms.GroupBox();
            this.cmbEsquema = new System.Windows.Forms.ComboBox();
            this.label19 = new System.Windows.Forms.Label();
            this.btnUpdateMunicipios = new System.Windows.Forms.Button();
            this.cmbColonias = new System.Windows.Forms.ComboBox();
            this.txtApellidoM = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtApellidoP = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.btnCodigoActivacion = new System.Windows.Forms.Button();
            this.txtRfc = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtCodigoPostal = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtNvoIngreso = new System.Windows.Forms.TextBox();
            this.txtEntreCalles = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.dtpFechaNacimiento = new System.Windows.Forms.DateTimePicker();
            this.txtObservaciones = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbMunicipio = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCodigoActivacion = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.txtAgente = new System.Windows.Forms.TextBox();
            this.txtAgenteCapturo = new System.Windows.Forms.TextBox();
            this.btnSalir = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.grpDatosAfiliado.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSucursal
            // 
            this.lblSucursal.AutoSize = true;
            this.lblSucursal.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSucursal.Location = new System.Drawing.Point(138, 23);
            this.lblSucursal.Name = "lblSucursal";
            this.lblSucursal.Size = new System.Drawing.Size(67, 37);
            this.lblSucursal.TabIndex = 1;
            this.lblSucursal.Text = ".....";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 119);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "No. de Solicitud";
            // 
            // txtNumeroSolicitud
            // 
            this.txtNumeroSolicitud.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNumeroSolicitud.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumeroSolicitud.Location = new System.Drawing.Point(137, 103);
            this.txtNumeroSolicitud.MaxLength = 13;
            this.txtNumeroSolicitud.Name = "txtNumeroSolicitud";
            this.txtNumeroSolicitud.Size = new System.Drawing.Size(181, 29);
            this.txtNumeroSolicitud.TabIndex = 3;
            this.txtNumeroSolicitud.Click += new System.EventHandler(this.txtNumeroSolicitud_Click);
            this.txtNumeroSolicitud.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNumeroSolicitud_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(324, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Código de A.S.";
            // 
            // txtCodigoAsistente
            // 
            this.txtCodigoAsistente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCodigoAsistente.Enabled = false;
            this.txtCodigoAsistente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigoAsistente.Location = new System.Drawing.Point(423, 113);
            this.txtCodigoAsistente.Name = "txtCodigoAsistente";
            this.txtCodigoAsistente.Size = new System.Drawing.Size(81, 22);
            this.txtCodigoAsistente.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(13, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Nombre de A.S.";
            // 
            // txtNombreAsistente
            // 
            this.txtNombreAsistente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNombreAsistente.Enabled = false;
            this.txtNombreAsistente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreAsistente.Location = new System.Drawing.Point(137, 137);
            this.txtNombreAsistente.Name = "txtNombreAsistente";
            this.txtNombreAsistente.Size = new System.Drawing.Size(367, 22);
            this.txtNombreAsistente.TabIndex = 5;
            // 
            // grpDatosAfiliado
            // 
            this.grpDatosAfiliado.Controls.Add(this.cmbEsquema);
            this.grpDatosAfiliado.Controls.Add(this.label19);
            this.grpDatosAfiliado.Controls.Add(this.btnUpdateMunicipios);
            this.grpDatosAfiliado.Controls.Add(this.cmbColonias);
            this.grpDatosAfiliado.Controls.Add(this.txtApellidoM);
            this.grpDatosAfiliado.Controls.Add(this.label17);
            this.grpDatosAfiliado.Controls.Add(this.txtApellidoP);
            this.grpDatosAfiliado.Controls.Add(this.label16);
            this.grpDatosAfiliado.Controls.Add(this.btnCodigoActivacion);
            this.grpDatosAfiliado.Controls.Add(this.txtRfc);
            this.grpDatosAfiliado.Controls.Add(this.label14);
            this.grpDatosAfiliado.Controls.Add(this.txtCodigoPostal);
            this.grpDatosAfiliado.Controls.Add(this.label13);
            this.grpDatosAfiliado.Controls.Add(this.txtNvoIngreso);
            this.grpDatosAfiliado.Controls.Add(this.txtEntreCalles);
            this.grpDatosAfiliado.Controls.Add(this.label7);
            this.grpDatosAfiliado.Controls.Add(this.txtTelefono);
            this.grpDatosAfiliado.Controls.Add(this.txtDireccion);
            this.grpDatosAfiliado.Controls.Add(this.label4);
            this.grpDatosAfiliado.Controls.Add(this.label10);
            this.grpDatosAfiliado.Controls.Add(this.label12);
            this.grpDatosAfiliado.Controls.Add(this.dtpFechaNacimiento);
            this.grpDatosAfiliado.Controls.Add(this.txtObservaciones);
            this.grpDatosAfiliado.Controls.Add(this.label5);
            this.grpDatosAfiliado.Controls.Add(this.label11);
            this.grpDatosAfiliado.Controls.Add(this.label9);
            this.grpDatosAfiliado.Controls.Add(this.cmbMunicipio);
            this.grpDatosAfiliado.Controls.Add(this.label8);
            this.grpDatosAfiliado.Controls.Add(this.txtNombre);
            this.grpDatosAfiliado.Controls.Add(this.label6);
            this.grpDatosAfiliado.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDatosAfiliado.Location = new System.Drawing.Point(19, 175);
            this.grpDatosAfiliado.Name = "grpDatosAfiliado";
            this.grpDatosAfiliado.Size = new System.Drawing.Size(504, 447);
            this.grpDatosAfiliado.TabIndex = 8;
            this.grpDatosAfiliado.TabStop = false;
            this.grpDatosAfiliado.Text = "Datos del afiliado";
            // 
            // cmbEsquema
            // 
            this.cmbEsquema.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cmbEsquema.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEsquema.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbEsquema.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbEsquema.FormattingEnabled = true;
            this.cmbEsquema.Items.AddRange(new object[] {
            "",
            "SUELDO",
            "COMISION"});
            this.cmbEsquema.Location = new System.Drawing.Point(308, 396);
            this.cmbEsquema.Name = "cmbEsquema";
            this.cmbEsquema.Size = new System.Drawing.Size(177, 24);
            this.cmbEsquema.TabIndex = 35;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(305, 377);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(135, 16);
            this.label19.TabIndex = 34;
            this.label19.Text = "Esquema de pago";
            // 
            // btnUpdateMunicipios
            // 
            this.btnUpdateMunicipios.Image = global::MRS.Properties.Resources.refresh1;
            this.btnUpdateMunicipios.Location = new System.Drawing.Point(449, 126);
            this.btnUpdateMunicipios.Name = "btnUpdateMunicipios";
            this.btnUpdateMunicipios.Size = new System.Drawing.Size(36, 23);
            this.btnUpdateMunicipios.TabIndex = 20;
            this.btnUpdateMunicipios.UseVisualStyleBackColor = true;
            this.btnUpdateMunicipios.Click += new System.EventHandler(this.btnUpdateMunicipios_Click);
            // 
            // cmbColonias
            // 
            this.cmbColonias.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cmbColonias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbColonias.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbColonias.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbColonias.FormattingEnabled = true;
            this.cmbColonias.Location = new System.Drawing.Point(115, 153);
            this.cmbColonias.Name = "cmbColonias";
            this.cmbColonias.Size = new System.Drawing.Size(370, 24);
            this.cmbColonias.TabIndex = 12;
            // 
            // txtApellidoM
            // 
            this.txtApellidoM.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtApellidoM.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtApellidoM.Location = new System.Drawing.Point(115, 70);
            this.txtApellidoM.Name = "txtApellidoM";
            this.txtApellidoM.Size = new System.Drawing.Size(370, 22);
            this.txtApellidoM.TabIndex = 8;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(6, 76);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(110, 16);
            this.label17.TabIndex = 32;
            this.label17.Text = "Apellido materno";
            // 
            // txtApellidoP
            // 
            this.txtApellidoP.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtApellidoP.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtApellidoP.Location = new System.Drawing.Point(115, 44);
            this.txtApellidoP.Name = "txtApellidoP";
            this.txtApellidoP.Size = new System.Drawing.Size(370, 22);
            this.txtApellidoP.TabIndex = 7;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(6, 50);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(107, 16);
            this.label16.TabIndex = 30;
            this.label16.Text = "Apellido paterno";
            // 
            // btnCodigoActivacion
            // 
            this.btnCodigoActivacion.Image = ((System.Drawing.Image)(resources.GetObject("btnCodigoActivacion.Image")));
            this.btnCodigoActivacion.Location = new System.Drawing.Point(12, 377);
            this.btnCodigoActivacion.Name = "btnCodigoActivacion";
            this.btnCodigoActivacion.Size = new System.Drawing.Size(287, 62);
            this.btnCodigoActivacion.TabIndex = 21;
            this.btnCodigoActivacion.Text = "Generar código de activación";
            this.btnCodigoActivacion.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCodigoActivacion.UseVisualStyleBackColor = true;
            this.btnCodigoActivacion.Click += new System.EventHandler(this.btnCodigoActivacion_Click);
            // 
            // txtRfc
            // 
            this.txtRfc.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRfc.Enabled = false;
            this.txtRfc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRfc.Location = new System.Drawing.Point(276, 338);
            this.txtRfc.Name = "txtRfc";
            this.txtRfc.Size = new System.Drawing.Size(209, 22);
            this.txtRfc.TabIndex = 18;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(226, 344);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(44, 16);
            this.label14.TabIndex = 26;
            this.label14.Text = "R.F.C.";
            // 
            // txtCodigoPostal
            // 
            this.txtCodigoPostal.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCodigoPostal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigoPostal.Location = new System.Drawing.Point(115, 339);
            this.txtCodigoPostal.MaxLength = 6;
            this.txtCodigoPostal.Name = "txtCodigoPostal";
            this.txtCodigoPostal.Size = new System.Drawing.Size(103, 22);
            this.txtCodigoPostal.TabIndex = 17;
            this.txtCodigoPostal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCodigoPostal_KeyPress);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(6, 345);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(92, 16);
            this.label13.TabIndex = 24;
            this.label13.Text = "Código postal";
            // 
            // txtNvoIngreso
            // 
            this.txtNvoIngreso.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNvoIngreso.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNvoIngreso.Location = new System.Drawing.Point(115, 313);
            this.txtNvoIngreso.Name = "txtNvoIngreso";
            this.txtNvoIngreso.Size = new System.Drawing.Size(370, 22);
            this.txtNvoIngreso.TabIndex = 16;
            // 
            // txtEntreCalles
            // 
            this.txtEntreCalles.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEntreCalles.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEntreCalles.Location = new System.Drawing.Point(115, 209);
            this.txtEntreCalles.Name = "txtEntreCalles";
            this.txtEntreCalles.Size = new System.Drawing.Size(370, 22);
            this.txtEntreCalles.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 189);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 16);
            this.label7.TabIndex = 12;
            this.label7.Text = "Calle / Número";
            // 
            // txtTelefono
            // 
            this.txtTelefono.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTelefono.Location = new System.Drawing.Point(330, 98);
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(155, 22);
            this.txtTelefono.TabIndex = 10;
            this.txtTelefono.Leave += new System.EventHandler(this.txtTelefono_Leave);
            // 
            // txtDireccion
            // 
            this.txtDireccion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDireccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDireccion.Location = new System.Drawing.Point(115, 183);
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Size = new System.Drawing.Size(370, 22);
            this.txtDireccion.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(133, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "Fecha de nacimiento";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(6, 215);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(78, 16);
            this.label10.TabIndex = 18;
            this.label10.Text = "Entre calles";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(6, 319);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(96, 16);
            this.label12.TabIndex = 22;
            this.label12.Text = "Nuevo ingreso";
            // 
            // dtpFechaNacimiento
            // 
            this.dtpFechaNacimiento.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaNacimiento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaNacimiento.Location = new System.Drawing.Point(145, 98);
            this.dtpFechaNacimiento.Name = "dtpFechaNacimiento";
            this.dtpFechaNacimiento.Size = new System.Drawing.Size(107, 22);
            this.dtpFechaNacimiento.TabIndex = 9;
            // 
            // txtObservaciones
            // 
            this.txtObservaciones.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtObservaciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtObservaciones.Location = new System.Drawing.Point(115, 237);
            this.txtObservaciones.Multiline = true;
            this.txtObservaciones.Name = "txtObservaciones";
            this.txtObservaciones.Size = new System.Drawing.Size(370, 70);
            this.txtObservaciones.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(262, 104);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 16);
            this.label5.TabIndex = 6;
            this.label5.Text = "Telefono";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(6, 237);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(100, 16);
            this.label11.TabIndex = 20;
            this.label11.Text = "Observaciones";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(6, 161);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(54, 16);
            this.label9.TabIndex = 16;
            this.label9.Text = "Colonia";
            // 
            // cmbMunicipio
            // 
            this.cmbMunicipio.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cmbMunicipio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMunicipio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbMunicipio.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMunicipio.FormattingEnabled = true;
            this.cmbMunicipio.Location = new System.Drawing.Point(115, 126);
            this.cmbMunicipio.Name = "cmbMunicipio";
            this.cmbMunicipio.Size = new System.Drawing.Size(327, 24);
            this.cmbMunicipio.TabIndex = 11;
            this.cmbMunicipio.SelectionChangeCommitted += new System.EventHandler(this.cmbMunicipio_SelectionChangeCommitted);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(6, 134);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(65, 16);
            this.label8.TabIndex = 14;
            this.label8.Text = "Municipio";
            // 
            // txtNombre
            // 
            this.txtNombre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombre.Location = new System.Drawing.Point(115, 18);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(370, 22);
            this.txtNombre.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 16);
            this.label6.TabIndex = 10;
            this.label6.Text = "Nombre";
            // 
            // txtCodigoActivacion
            // 
            this.txtCodigoActivacion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCodigoActivacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigoActivacion.Location = new System.Drawing.Point(188, 624);
            this.txtCodigoActivacion.Name = "txtCodigoActivacion";
            this.txtCodigoActivacion.ReadOnly = true;
            this.txtCodigoActivacion.Size = new System.Drawing.Size(258, 35);
            this.txtCodigoActivacion.TabIndex = 19;
            this.txtCodigoActivacion.Click += new System.EventHandler(this.txtCodigoActivacion_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(28, 643);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(155, 16);
            this.label15.TabIndex = 29;
            this.label15.Text = "Código de activación";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(13, 82);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(57, 16);
            this.label18.TabIndex = 35;
            this.label18.Text = "Agente";
            // 
            // txtAgente
            // 
            this.txtAgente.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAgente.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            this.txtAgente.Location = new System.Drawing.Point(137, 69);
            this.txtAgente.Name = "txtAgente";
            this.txtAgente.Size = new System.Drawing.Size(181, 29);
            this.txtAgente.TabIndex = 1;
            // 
            // txtAgenteCapturo
            // 
            this.txtAgenteCapturo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAgenteCapturo.Enabled = false;
            this.txtAgenteCapturo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            this.txtAgenteCapturo.Location = new System.Drawing.Point(323, 69);
            this.txtAgenteCapturo.Name = "txtAgenteCapturo";
            this.txtAgenteCapturo.Size = new System.Drawing.Size(181, 29);
            this.txtAgenteCapturo.TabIndex = 2;
            // 
            // btnSalir
            // 
            this.btnSalir.Image = ((System.Drawing.Image)(resources.GetObject("btnSalir.Image")));
            this.btnSalir.Location = new System.Drawing.Point(452, 624);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(71, 45);
            this.btnSalir.TabIndex = 22;
            this.btnSalir.Text = "Salir";
            this.btnSalir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, -14);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 74);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // FrmActivaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(540, 680);
            this.Controls.Add(this.txtAgenteCapturo);
            this.Controls.Add(this.txtAgente);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.grpDatosAfiliado);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txtNombreAsistente);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCodigoAsistente);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtNumeroSolicitud);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblSucursal);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtCodigoActivacion);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmActivaciones";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Alta de afiliado SAP";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmActivaciones_FormClosed);
            this.Load += new System.EventHandler(this.FrmActivaciones_Load);
            this.grpDatosAfiliado.ResumeLayout(false);
            this.grpDatosAfiliado.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblSucursal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNumeroSolicitud;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCodigoAsistente;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNombreAsistente;
        private System.Windows.Forms.GroupBox grpDatosAfiliado;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpFechaNacimiento;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtApellidoM;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtApellidoP;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtCodigoActivacion;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnCodigoActivacion;
        private System.Windows.Forms.TextBox txtRfc;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtCodigoPostal;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtNvoIngreso;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtObservaciones;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtEntreCalles;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbMunicipio;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtAgente;
        private System.Windows.Forms.ComboBox cmbColonias;
        private System.Windows.Forms.TextBox txtAgenteCapturo;
        private System.Windows.Forms.Button btnUpdateMunicipios;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox cmbEsquema;


    }
}

