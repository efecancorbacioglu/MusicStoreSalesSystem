using MusicStoreSalesSystemSolution.Businesses.Concrete;
using MusicStoreSalesSystemSolution.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicStoreSalesSystemSolution.UI.Forms
{
    public partial class AdminProductsForm : Form
    {
        public AdminProductsForm()
        {
            InitializeComponent();
        }
        ProductBusiness productBusiness = new ProductBusiness();
        CategoryBusiness categoryBusiness = new CategoryBusiness();

        Product currenProduct;
        public void ListProducts()
        {
            dgvProducts.DataSource = productBusiness.GetAll();
            dgvProducts.Columns["CategoryId"].Visible = false;
            dgvProducts.Columns["Sales"].Visible = false;
        }

        public string GenerateProductNo()
        {

            Random rnd = new Random();
            int no = rnd.Next(10000000, 99999999);
            var product = productBusiness.Get(p => p.ProductNo == no.ToString());

            if (product == null)
            {
                return no.ToString();
            }
            else
            {
                return GenerateProductNo();
            }

        }
        private void AdminProductsForm_Load(object sender, EventArgs e)
        {
            ListProducts();
            var categories = categoryBusiness.GetAll();
            categories.Insert(0, new Category { CategoryId = 0, CategoryName = "Choose Category..." });
            cbCategories.DataSource = categories;
            cbCategories.DisplayMember = "CategoryName";
            cbCategories.ValueMember = "CategoryId";
        }

        private void dgvProducts_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            var selectedRow = dgvProducts.Rows[e.RowIndex];

            txtId.Text = selectedRow.Cells["ProductId"].Value.ToString();
            txtProductName.Text = selectedRow.Cells["ProductName"].Value.ToString();
            txtPrice.Text = selectedRow.Cells["Price"].Value.ToString();
            txtStock.Text = selectedRow.Cells["Stock"].Value.ToString();
            cbCategories.SelectedValue = selectedRow.Cells["CategoryId"].Value;

        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            var product = productBusiness.Get(p => p.ProductName == txtProductName.Text);
            if (product == null)
            {
                if (string.IsNullOrEmpty(txtPrice.Text) || string.IsNullOrEmpty(txtStock.Text) || cbCategories.SelectedIndex == 0 || pbInstrument.ImageLocation != null)
                {
                    MessageBox.Show("Please enter all product information completely", "Missing Informations", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    var answer = MessageBox.Show("Are you sure to add this product?", "Adding", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (answer == DialogResult.Yes)
                    {
                        var productNo = GenerateProductNo();
                        productBusiness.Add(new Product
                        {
                            ProductName = txtProductName.Text,
                            Price = Convert.ToDecimal(txtPrice.Text),
                            Stock = Convert.ToInt32(txtStock.Text),
                            Photo = ImageBusiness.ImageToByte(pbInstrument.ImageLocation),
                            CategoryId = cbCategories.SelectedIndex,
                            ProductNo = productNo
                        });
                    }
                }
            }
        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
