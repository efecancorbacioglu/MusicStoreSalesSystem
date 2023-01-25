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
        string imagePath;
        public void ListProducts()
        {
            dgvProducts.DataSource = productBusiness.GetAll();
            dgvProducts.Columns["Photo"].Visible = false;
            dgvProducts.Columns["IsActive"].Visible = false;
        }
        public void clearTextBoxes()
        {
            txtId.Clear();
            txtProductName.Clear();
            txtPrice.Clear();
            txtStock.Clear();
            cbCategories.SelectedIndex = 0;
            pbInstrument.ImageLocation = null;

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
                            Photo = pbInstrument.ImageLocation != null ? ImageBusiness.ImageToByte(pbInstrument.ImageLocation) : null,
                            CategoryId = int.Parse(cbCategories.SelectedValue.ToString()),
                            IsActive = true,
                            ProductNo = productNo
                        });

                        MessageBox.Show("The product insert the database.", "Transaction Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ListProducts();
                        clearTextBoxes();
                    }
                }
            }
            else if (product.IsActive == false)
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
                        productBusiness.Edit(new Product
                        {
                            ProductId = product.ProductId,
                            ProductName = txtProductName.Text,
                            Price = Convert.ToDecimal(txtPrice.Text),
                            Stock = Convert.ToInt32(txtStock.Text),
                            Photo = pbInstrument.ImageLocation != null ? ImageBusiness.ImageToByte(pbInstrument.ImageLocation) : null,
                            CategoryId = int.Parse(cbCategories.SelectedValue.ToString()),
                            ProductNo = product.ProductNo,
                            IsActive = true
                        });

                        MessageBox.Show("The product insert the database.", "Transaction Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ListProducts();
                        clearTextBoxes();
                    }
                }
            }
            else
            {
                MessageBox.Show("A product of this information is available in the database. Please check the product name or barcode.", "The product exist", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvProducts_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            var selectedRow = dgvProducts.Rows[e.RowIndex];
            var product = productBusiness.GetById(int.Parse(selectedRow.Cells["ProductId"].Value.ToString()));

            txtId.Text = selectedRow.Cells["ProductId"].Value.ToString();
            txtProductName.Text = selectedRow.Cells["ProductName"].Value.ToString();
            txtPrice.Text = selectedRow.Cells["Price"].Value.ToString();
            txtStock.Text = selectedRow.Cells["Stock"].Value.ToString();
            cbCategories.SelectedValue = selectedRow.Cells["CategoryId"].Value;
            pbInstrument.Image = product.Photo != null ? ImageBusiness.ByteToImage((byte[])selectedRow.Cells["Photo"].Value) : null;

        }

        private void txtClear_Click(object sender, EventArgs e)
        {
            clearTextBoxes();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                MessageBox.Show("Please choose a product before deleting", "Choose Product", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                var answer = MessageBox.Show("Are you sure to delete this product?", "Deleting", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (answer == DialogResult.Yes)
                {
                    productBusiness.Delete(Convert.ToInt32(txtId.Text));
                    MessageBox.Show("The product deleted from the database", "The product Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListProducts();

                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtProductName.Text) || string.IsNullOrEmpty(txtPrice.Text) || string.IsNullOrEmpty(txtStock.Text) ||
                cbCategories.SelectedIndex == 0)
            {
                MessageBox.Show("Please enter all product information completely", "Missing Informations", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                var answer = MessageBox.Show("Are you sure to update this product?", "Updating", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (answer == DialogResult.Yes)
                {
                    var product = productBusiness.GetById(Convert.ToInt32(txtId.Text));

                    productBusiness.Edit(new Product
                    {
                        ProductId = Convert.ToInt32(txtId.Text),
                        ProductName = txtProductName.Text,
                        Price = Convert.ToDecimal(txtPrice.Text),
                        Stock = Convert.ToInt32(txtStock.Text),
                        Photo = pbInstrument.ImageLocation != null ? ImageBusiness.ImageToByte(pbInstrument.ImageLocation) : null,
                        CategoryId = int.Parse(cbCategories.SelectedValue.ToString()),
                        ProductNo = product.ProductNo,
                        IsActive = true
                    });

                    MessageBox.Show("The Product Updated", "Transaction Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListProducts();
                }
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files(*.JPEG;*.JPG;*.PNG) | *.JPEG;*.JPG;*.PNG";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    imagePath = openFileDialog.FileName;
                    pbInstrument.ImageLocation = openFileDialog.FileName;
                }
            }
        }

    }
}
