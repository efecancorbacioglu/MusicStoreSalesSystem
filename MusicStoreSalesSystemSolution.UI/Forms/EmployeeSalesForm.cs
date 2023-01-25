using MusicStoreSalesSystemSolution.Businesses.Concrete;
using MusicStoreSalesSystemSolution.Entities;
using MusicStoreSalesSystemSolution.UI.Domain;
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
    public partial class EmployeeSalesForm: Form
    {
        public static SaleInfo SaleInfo { get; set; }
        public static Sale sale { get; set; }
        public EmployeeSalesForm()
        {
            InitializeComponent();
        }

        EmployeeBusiness employeeBusiness = new EmployeeBusiness();
        CustomerBusiness customerBusiness = new CustomerBusiness();
        ProductBusiness productBusiness = new ProductBusiness();
        SalesBusiness salesBusiness = new SalesBusiness();

        public void CalculateTotalAmount()
        {
            int total = 0;
            for (int i = 0; i < dgvSales.Rows.Count; i++)
            {
                var price = Convert.ToInt32(dgvSales.Rows[i].Cells["Price"].Value);
                var quantity = Convert.ToInt32(dgvSales.Rows[i].Cells["Quantity"].Value);

                total += (price * quantity);

            }

            txtTotalAmount.Text = total.ToString();
        }
        public void ClearCustomerInfo()
        {
            txtCustomerName.Clear();
            txtCustomerSurname.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
            txtSearchPhone.Clear();
        }

        private void EmployeeSalesForm_Load(object sender, EventArgs e)
        {
            var employee = employeeBusiness.Get(em => em.EmployeeId == LoginForm.UserInfo.EmployeeId);
            lblUserInfo.Text = employee.FirstName + " " + employee.LastName;
            pbUserPic.Image = employee.Photo != null ? ImageBusiness.ByteToImage(employee.Photo) : null;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            var answer = MessageBox.Show("Are you sure to exit the system?", "Exit System", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer == DialogResult.Yes)
            {
                this.Close();
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var Customerphone = txtSearchPhone.Text;
            var currentcustomer = customerBusiness.Get(c => c.Phone == Customerphone);

            if (currentcustomer != null)
            {
                MessageBox.Show("The customer found", "Customer exist", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCustomerName.Text = currentcustomer.FirstName;
                txtCustomerSurname.Text = currentcustomer.LastName;
                txtEmail.Text = currentcustomer.Email;
                txtPhone.Text = currentcustomer.Phone;
                txtAddress.Text = currentcustomer.Address;
            }
            else
            {
                MessageBox.Show("The customer does not exist in database", "Customer Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            txtSearchPhone.Clear();
        }

        private void btnClearCustomer_Click_1(object sender, EventArgs e)
        {

            ClearCustomerInfo();

        }

        private void btn1_Click_1(object sender, EventArgs e)
        {
            var text = txtProductNo.Text;
            text += "1";
            txtProductNo.Text = text;
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            var text = txtProductNo.Text;
            text += "2";
            txtProductNo.Text = text;
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            var text = txtProductNo.Text;
            text += "3";
            txtProductNo.Text = text;
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            var text = txtProductNo.Text;
            text += "4";
            txtProductNo.Text = text;
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            var text = txtProductNo.Text;
            text += "5";
            txtProductNo.Text = text;
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            var text = txtProductNo.Text;
            text += "6";
            txtProductNo.Text = text;
        }

        private void btn7_Click_1(object sender, EventArgs e)
        {
            var text = txtProductNo.Text;
            text += "7";
            txtProductNo.Text = text;
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            var text = txtProductNo.Text;
            text += "8";
            txtProductNo.Text = text;
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            var text = txtProductNo.Text;
            text += "9";
            txtProductNo.Text = text;
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            var text = txtProductNo.Text;
            text += "0";
            txtProductNo.Text = text;
        }

        private void btnClearProductNo_Click(object sender, EventArgs e)
        {
            txtProductNo.Clear();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

                if (string.IsNullOrEmpty(txtProductNo.Text))
                {
                    MessageBox.Show("Please enter a product number", "Missing Product No", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    var product = productBusiness.Get(p => p.ProductNo == txtProductNo.Text);

                    if (product != null)
                    {

                        var index = dgvSales.Rows.Add();
                        dgvSales.Rows[index].Cells["ProductName"].Value = product.ProductName;
                        dgvSales.Rows[index].Cells["Price"].Value = product.Price;
                        dgvSales.Rows[index].Cells["Quantity"].Value = 1;


                        CalculateTotalAmount();

                    }
                    else
                    {
                        MessageBox.Show("There is no product for this product number.", "Product Does Not Exist", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    txtProductNo.Text = null;
                }
            }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            dgvSales.Rows.Clear();
            txtProductNo.Text = null;
            txtTotalAmount.Text = null;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (dgvSales.Rows.Count != 0)
            {
                if (string.IsNullOrEmpty(txtCustomerName.Text) || string.IsNullOrEmpty(txtCustomerSurname.Text) ||
                    string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtPhone.Text) || string.IsNullOrEmpty(txtAddress.Text))
                {
                    MessageBox.Show("Please enter complete customer information.", "Missing Customer Informations", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {

                    var answer = MessageBox.Show("Are you confirming the sale?", "Sale Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (answer == DialogResult.Yes)
                    {
                        var currentCustomer = customerBusiness.Get(c => c.Phone == txtPhone.Text);

                        if (currentCustomer == null)
                        {
                            customerBusiness.Add(new Customer
                            {
                                FirstName = txtCustomerName.Text,
                                LastName = txtCustomerSurname.Text,
                                Address = txtAddress.Text,
                                Phone = txtPhone.Text,
                                Email = txtEmail.Text
                            });
                        }
                        var employee = employeeBusiness.Get(em => em.EmployeeId == LoginForm.UserInfo.EmployeeId);
                        var newcurrentCustomer = customerBusiness.Get(c => c.Phone == txtPhone.Text);
                        SaleInfo = new SaleInfo
                        {
                            CustomerFirstName = txtCustomerName.Text,
                            CustomerLastName = txtCustomerSurname.Text,
                            EmployeeFirstName = employee.FirstName,
                            EmployeeLastName = employee.LastName,
                            SaleDate = DateTime.Now,
                            TotalAmountPrice = Convert.ToDecimal(txtTotalAmount.Text)
                        };

                        for (int i = 0; i < dgvSales.Rows.Count; i++)
                        {
                            string product = dgvSales.Rows[i].Cells["ProductName"].Value.ToString();
                            var currentProduct = productBusiness.Get(p => p.ProductName == product);
                            var quantity = Convert.ToInt32(dgvSales.Rows[i].Cells["Quantity"].Value.ToString());


                            salesBusiness.Add(new Sale
                            {
                                Quantity = quantity,
                                SaleDate = DateTime.Now,
                                CustomerId = newcurrentCustomer.CustomerId,
                                ProductId = currentProduct.ProductId,
                                EmployeeId = employee.EmployeeId

                            });


                            SaleInfo.ActiveSales.Add(new ActiveSale
                            {
                                ProductName = currentProduct.ProductName,
                                ProductPrice = currentProduct.Price,
                                Quantity = quantity,
                                TotalPrice = quantity * currentProduct.Price
                            });

                            productBusiness.Edit(new Product
                            {
                                ProductId = currentProduct.ProductId,
                                ProductName = currentProduct.ProductName,
                                Price = currentProduct.Price,
                                Photo = currentProduct.Photo,
                                Stock = currentProduct.Stock - quantity,
                                CategoryId = currentProduct.CategoryId,
                                ProductNo = currentProduct.ProductNo

                            });
                        }




                        MessageBox.Show("Sale completed", "Transaction Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ReportForm reportForm = new ReportForm();
                        reportForm.Show();

                        ClearCustomerInfo();
                        dgvSales.Rows.Clear();
                        txtProductNo.Text = null;
                        txtTotalAmount.Text = null;

                    }
                }
            }
            else
            {
                MessageBox.Show("Please add the products to be sold to the cart.", "Cart is Empty", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvSales_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                int currentQuantity = Convert.ToInt32(dgvSales.Rows[e.RowIndex].Cells["Quantity"].Value);
                currentQuantity += 1;
                dgvSales.Rows[e.RowIndex].Cells["Quantity"].Value = currentQuantity;

                CalculateTotalAmount();
            }

            if (e.ColumnIndex == 4)
            {
                int currentQuantity = Convert.ToInt32(dgvSales.Rows[e.RowIndex].Cells["Quantity"].Value);
                if (currentQuantity > 1)
                {
                    currentQuantity -= 1;
                    dgvSales.Rows[e.RowIndex].Cells["Quantity"].Value = currentQuantity;

                }
                CalculateTotalAmount();
            }
            if (e.ColumnIndex == 5)
            {
                dgvSales.Rows.RemoveAt(dgvSales.Rows[e.RowIndex].Index);
                CalculateTotalAmount();
            }
        }
    }
    }

