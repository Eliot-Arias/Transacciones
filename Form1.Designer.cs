namespace Transacciones
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblProducto = new Label();
            lblCantidad = new Label();
            cmbProducto = new ComboBox();
            txtCantidad = new TextBox();
            btnVentasSinSP = new Button();
            btnVentaConSP = new Button();
            SuspendLayout();
            // 
            // lblProducto
            // 
            lblProducto.AutoSize = true;
            lblProducto.Location = new Point(44, 30);
            lblProducto.Name = "lblProducto";
            lblProducto.Size = new Size(56, 15);
            lblProducto.TabIndex = 0;
            lblProducto.Text = "Producto";
            // 
            // lblCantidad
            // 
            lblCantidad.AutoSize = true;
            lblCantidad.Location = new Point(44, 87);
            lblCantidad.Name = "lblCantidad";
            lblCantidad.Size = new Size(55, 15);
            lblCantidad.TabIndex = 1;
            lblCantidad.Text = "Cantidad";
            // 
            // cmbProducto
            // 
            cmbProducto.FormattingEnabled = true;
            cmbProducto.Location = new Point(136, 29);
            cmbProducto.Name = "cmbProducto";
            cmbProducto.Size = new Size(121, 23);
            cmbProducto.TabIndex = 2;
            // 
            // txtCantidad
            // 
            txtCantidad.Location = new Point(136, 84);
            txtCantidad.Name = "txtCantidad";
            txtCantidad.Size = new Size(121, 23);
            txtCantidad.TabIndex = 3;
            // 
            // btnVentasSinSP
            // 
            btnVentasSinSP.Location = new Point(44, 149);
            btnVentasSinSP.Name = "btnVentasSinSP";
            btnVentasSinSP.Size = new Size(213, 23);
            btnVentasSinSP.TabIndex = 4;
            btnVentasSinSP.Text = "Transacción Venta";
            btnVentasSinSP.UseVisualStyleBackColor = true;
            btnVentasSinSP.Click += btnVentasSinSP_Click;
            // 
            // btnVentaConSP
            // 
            btnVentaConSP.Location = new Point(44, 192);
            btnVentaConSP.Name = "btnVentaConSP";
            btnVentaConSP.Size = new Size(213, 23);
            btnVentaConSP.TabIndex = 5;
            btnVentaConSP.Text = "Transaccion Venta SP";
            btnVentaConSP.UseVisualStyleBackColor = true;
            btnVentaConSP.Click += btnVentaConSP_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(457, 378);
            Controls.Add(btnVentaConSP);
            Controls.Add(btnVentasSinSP);
            Controls.Add(txtCantidad);
            Controls.Add(cmbProducto);
            Controls.Add(lblCantidad);
            Controls.Add(lblProducto);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblProducto;
        private Label lblCantidad;
        private ComboBox cmbProducto;
        private TextBox txtCantidad;
        private Button btnVentasSinSP;
        private Button btnVentaConSP;
    }
}