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
using System.Windows.Forms.DataVisualization.Charting;

namespace MusicStoreSalesSystemSolution.UI.Forms
{
    public partial class AdminSalesForm : Form
    {
        public void ClearChart(Chart chart)
        {
            foreach (var series in chart.Series)
            {
                series.Points.Clear();
            }
        }
        public AdminSalesForm()
        {
            InitializeComponent();
        }

        SalesBusiness salesBusiness = new SalesBusiness();
        ProductBusiness productBusiness = new ProductBusiness();
        EmployeeBusiness employeeBusiness = new EmployeeBusiness();

        private void Top5Product_CheckedChanged(object sender, EventArgs e)
        {
            var top5products = salesBusiness.GetTopFiveSale();
            var mostSalesProduct = top5products.FirstOrDefault();
            var productPhoto = productBusiness.Get(p => p.ProductName == mostSalesProduct.ProductName).Photo;

            if (mostSalesProduct != null)
            {
                pbImage.Image = productPhoto != null ? ImageBusiness.ByteToImage((byte[])productPhoto) : null;
                lblTitle.Text = "The Most Selling Product";
                lblName.Text = mostSalesProduct.ProductName;
                lblTotalSales.Text = mostSalesProduct.TotalSales.ToString();
            }
            else
            {
                pbImage.Image = null;
                lblTitle.Text = "The Most Selling Product";
                lblName.Text = "None";
                lblTotalSales.Text = "0";
            }

            ClearChart(chart1);

            foreach (var item in top5products)
            {
                chart1.Series["Series1"].Points.AddXY(item.ProductName, item.TotalSales);
            }
        }

        private void Top5Employee_CheckedChanged(object sender, EventArgs e)
        {
            var top5seller = salesBusiness.GetTopFiveSeller();
            var mostSeller = top5seller.FirstOrDefault();
            var employeePhoto = employeeBusiness.Get(em => em.FirstName +" "+ em.LastName == mostSeller.EmployeeName).Photo;
            
            if (mostSeller != null)
            {
                pbImage.Image = employeePhoto != null ? ImageBusiness.ByteToImage((byte[])employeePhoto) : null;
                lblTitle.Text = "The Most Seller";
                lblName.Text = mostSeller.EmployeeName;
                lblTotalSales.Text = mostSeller.TotalSales.ToString();
            }
            else
            {
                pbImage = null;
                lblTitle.Text = "The Most Seller";
                lblName.Text = "None";
                lblTotalSales.Text = "0";
            }

            ClearChart(chart1);
            foreach (var item in top5seller)
            {
                chart1.Series["Series1"].Points.AddXY(item.EmployeeName, item.TotalSales);
            }
        }

        private void AdminSalesForm_Load(object sender, EventArgs e)
        {
            Top5Product.Checked = true;
        }
    }
}
