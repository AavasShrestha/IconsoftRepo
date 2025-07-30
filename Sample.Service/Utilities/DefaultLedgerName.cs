using Azure;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Identity.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.JavaScript;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CBS.Service.Utilities
{
    public static class DefaultData
    {
        public static List<string> RestrictedName = new List<string>
            {
                "Assets",
                "Capital and Liabilities",
                "Income",
                "Current Assets",
                "Inventory",
                "Investment",
                "Staff Loan and Advances",
                "Fixed Assets",
                "Sundry Debtors",
                "Sundry Creditors",
                "Deposits",
                "Current Liabilities",
                "Capital Accounts",
                "Provisions",
                "Reserve and Surplus",
                "Administrative Exp",
                "Interest Exp",
                "Depreciation Exp",
                "Term Loans",
                "Discount Allowed",
                "Discount Receipt",
                "Tax Provisions",
                "Salary & Allowances",
                "Discount Allowed",
                "Bank Accounts",
                "Cash Accounts",
                "Profit and Loss Account",
                "P/L Account",
                "Tax Provisions",
                "Sales A/C",
                "Purchase A/C",
                "Furniture A/C",
                "Plant & Machinery A/C",
                "Land & Buildings A/C",
                "Vehicles A/C",
                "Computer & Office Equipments A/C",
                "Other Assets A/C",
                "Preliminary Exp",
                "Long-Term Loan",
                "Short-Term Loan",
                "Prepaid Exp",
                "Outstanding Exp",
                "Penalty A/C",
                "Interest A/C",
                "Loan Provision",
                "MBills Payable",
                "MK1 Payable",
                "MK2 Payable",
                "ATM Terminals",
                "SCT Account",
                "ATM Charge Payable",
                "Production",
                "Interest Tax",
                "Bonus Tax (edited)"
            };
    }
}