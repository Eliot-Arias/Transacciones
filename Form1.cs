using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace Transacciones
{
    public partial class Form1 : Form
    {

        // cadena de conexion
        private static string strConexion = @"Server=PC10\VE_SERVER;Database=Transacciones;User Id=sesionlp;Password=lp2023;";
        //private static string strConexion = @"Server=DESKTOPPE21LUS\MSSQLSERVER19;Database=AplicacionCapas;Trusted_Connection=True;";
        SqlConnection cn;





        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                cn = new SqlConnection(strConexion);
            }
            catch (Exception ex)
            {
                //verifica si cineccion es valida
                MessageBox.Show("Error en coneccion: " + ex.Message, "",
               MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            cmbProducto.DataSource = listarProductos();
            cmbProducto.DisplayMember = "Name";
            cmbProducto.ValueMember = "ProductID";
            cmbProducto.SelectedIndex = 0;
        }

        public DataTable listarProductos()
        {
            using (SqlCommand cmd = new SqlCommand("spProduct", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable tbl = new DataTable();
                    da.Fill(tbl);
                    return tbl;
                }
            }
        }

        public int grabarVentaSinSP(int ProductID, int quantity)
        {
            SqlCommand cmd;
            SqlCommand cmd2;
            SqlCommand cmd3;
            SqlCommand cmd4;
            SqlTransaction transaction;
            cn.Open();
            transaction = cn.BeginTransaction();
            try
            {
                //1. valida stock disponible
                string commandText = " SELECT Quantity FROM Product WHERE ProductId = @ProductId";
                cmd = new SqlCommand(commandText, cn, transaction);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ProductId", ProductID);
                int disponible = (int)cmd.ExecuteScalar();
                if (disponible < quantity)
                {
                    MessageBox.Show("Error saldo insuficiente ", "", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                    transaction.Rollback();
                    return -1;
                }
                //2. actualiza stock producto
                commandText = "UPDATE Product SET Quantity = (Quantity - @QuantityToSell) WHERE ProductID = @ProductID";
                cmd2 = new SqlCommand(commandText, cn, transaction);
                cmd2.CommandType = CommandType.Text;
                cmd2.Parameters.AddWithValue("@ProductId", ProductID);
                cmd2.Parameters.AddWithValue("@QuantityToSell", quantity);
                disponible = (int)cmd2.ExecuteNonQuery();
                //3. busca siguente id de ventas
                commandText = "SELECT CASE WHEN MAX(ProductSalesId) IS NULL THEN 0 ELSE MAX(ProductSalesId) END FROM ProductSales";
                cmd3 = new SqlCommand(commandText, cn, transaction);
                cmd3.CommandType = CommandType.Text;
                cmd3.Parameters.AddWithValue("@ProductId", ProductID);
                cmd3.Parameters.AddWithValue("@QuantityToSell", quantity);
                int SiguienteID = (int)cmd3.ExecuteScalar();
                SiguienteID++;
                //4. inserta en tabla sales product
                commandText = " INSERT INTO ProductSales(ProductSalesId, ProductId, QuantitySold) VALUES(@ProductSalesId, @ProductId, @QuantityToSell)";
                cmd4 = new SqlCommand(commandText, cn, transaction);
                cmd4.CommandType = CommandType.Text;
                cmd4.Parameters.AddWithValue("@ProductSalesId", SiguienteID);
                cmd4.Parameters.AddWithValue("@ProductId", ProductID);
                cmd4.Parameters.AddWithValue("@QuantityToSell", quantity);
                disponible = (int)cmd4.ExecuteNonQuery();
                transaction.Commit(); // confirmacion de exito transaccion
                MessageBox.Show("Se registro con exito", "app", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se finaliza transaccion con error: " + ex.Message, "", MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
                transaction.Rollback();// caso e que deshacemos los cambios por error en parte de transaccion
            }
            cn.Close();
            return 0;
        }

        private void btnVentasSinSP_Click(object sender, EventArgs e)
        {
            if (txtCantidad.Text != "" && cmbProducto.SelectedIndex >= 0)
            {

                grabarVentaSinSP(Convert.ToInt32(cmbProducto.SelectedValue),
                Convert.ToInt32(txtCantidad.Text));
            }

        }

        public int grabarVentaConSP(int ProductID, int quantity)
        {
            cn.Open();
            using (SqlCommand cmd = new SqlCommand("spSellProduct", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductID", ProductID);
                cmd.Parameters.AddWithValue("@QuantityToSell", quantity);
                int i = 0;
                try
                {
                    i = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error en guardar venta" +
                   ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cn.Close();
                    return -1;
                }
                cn.Close();
                MessageBox.Show("Se grabo peracion con exito", "",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
                return 0;
            }
        }

        private void btnVentaConSP_Click(object sender, EventArgs e)
        {
            if (txtCantidad.Text != "" && cmbProducto.SelectedIndex >= 0)
            {

                grabarVentaConSP(Convert.ToInt32(cmbProducto.SelectedValue),
                Convert.ToInt32(txtCantidad.Text));
            }
        }
    }
}