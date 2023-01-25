using Microsoft.Reporting.WinForms;
using MusicStoreSalesSystemSolution.UI.Datasets;
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
    public partial class ReportForm : Form
    {
        SaleInfo saleInfo;
        public ReportForm()
        {
            InitializeComponent();
            saleInfo = EmployeeSalesForm.SaleInfo;
        }

        private void ReportForm_Load(object sender, EventArgs e)
        {
            MusicStoreDatasets dataset = new MusicStoreDatasets();


            foreach (var p in saleInfo.ActiveSales)
            {
                dataset.invoice.AddinvoiceRow(p.Quantity, p.ProductName, p.ProductPrice, string.Format("{0:C}", p.TotalPrice));
            }

            reportViewer1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local;
            LocalReport localReport = reportViewer1.LocalReport;
            localReport.ReportPath = @"Reports\SalesInvoiceReport.rdlc";
            ReportDataSource reportDataSource = new ReportDataSource("MusicStoreDatasets");
            reportDataSource.Name = "MusicStoreDatasets";
            reportDataSource.Value = dataset.invoice;
            localReport.DataSources.Add(reportDataSource);


            ReportParameterCollection reportParameters = new ReportParameterCollection();
            reportParameters.Add(new ReportParameter("CustomerFirstNameParam", saleInfo.CustomerFirstName));
            reportParameters.Add(new ReportParameter("CustomerLastNameParam", saleInfo.CustomerLastName));
            reportParameters.Add(new ReportParameter("EmployeeFirstNameParam", saleInfo.EmployeeFirstName));
            reportParameters.Add(new ReportParameter("EmployeeLastNameParam", saleInfo.EmployeeLastName));
            reportParameters.Add(new ReportParameter("SaleDateParam", saleInfo.SaleDate.ToString()));
            reportParameters.Add(new ReportParameter("TotalAmountParam", string.Format("{0:C}", saleInfo.TotalAmountPrice)));

            reportViewer1.LocalReport.SetParameters(reportParameters);


            this.reportViewer1.RefreshReport();
        }
    }
}
