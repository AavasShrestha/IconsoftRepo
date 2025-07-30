using Azure;
using CBS.Data.DTO;
using CBS.Data.TenantDB;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CBS.Service.Utilities
{
    public class MenuData
    {
        public List<ApplicationMenuDTO> MenuDTOs { get; set; }

        public MenuData()
        {
            this.MenuDTOs = new List<ApplicationMenuDTO>();
            var generalMenu = new List<string>
            {
                "Master Account",
                "User Role Setting",
                "User Setting",
                "Fiscal Year Setting",
                "Branch Setting",
                "Collection Area",
                "Area Address Setting",
                "Penalty and Interest Setting",
                "Deposit & Interest Setting",
                "Committee/Samuha Setting",
                "Tax Rate On Interest Setting",
                "Unit Setting",
                //Posting
                 "Journal Voucher",
                "Opening Balance",
                "Account Transfer",
                "Amount Transfer (Branch)",
                "Stock Transfer (Branch)",
                "Individual Entry",
                "Sales & Purchase",
                //Deposit
                "Deposit Collection",
                "Deposit Payment",
                "Monthly Settlement",
                "Group Deposit Collection",
                //Loan
                "Loan Disbursement",
                "Loan & Interest Collection",
                "Loan Collection Report",
                "Regular Loan Payment Schedule Report",
                //Operations
                "Interest Payable",
                "Share Bonus",
                "Monthly Loan Settlement",
                "Depreciation Calculation",
                "Fund/Liability Allocation",
                "Cash Vault",
                "Fund Distribution",
                "Interest Receivable",
                "Profit Allocation",
                "Change Interest Rate On Loan",
                "Annual Planning",
                "Sales Price Update",

                //Tools
                "Entry Checking",
                "Check Loan Schedule",
                "Show Cancelled Records",
                "Create Backup",

                //Payment slips
                "Issue & Print Slip",
                "Manage Slip",
                "Statement Slip",

                //Certificate
                "TDS on Interest Certificate",
                "Balance Certificate",
                "Loan Clearence Certificate",
                "Share Certificate",

                //SMS Service
                "SMS Database Setting",
                "Member Registration",
                "Registration Report",
                "Send Birthday SMS",

                //Tablet
                "Import Data",
                "Export Data",
            };

            this.MenuDTOs = generalMenu.Select(menuName => new ApplicationMenuDTO
            {
                MenuName = menuName,
                ActionList = new List<Data.Enum.Action>
                {
                    Data.Enum.Action.Create,
                    Data.Enum.Action.Update,
                    Data.Enum.Action.Delete,
                    Data.Enum.Action.View
                }
            }).ToList();

            var posting = new List<string>
            {
            };

            var settlementReport = new List<string>
            {
                "Account Statement",
                "DayBook Reports",
                "BalanceSheet Reports",
                "Stock Transfer Report",
                "Collection Sheet",
                "Collector Report",
                "Stock Report",
                "Monthly Sales/Purchase Report",
                "Control Ledger [4 Ledgers]",
                "Loan Investment Report",
                "Cash (Vault) Reports",
                "Due Interest Verification",
                "4 Ledger Balance",
                "Interest on Deposit Holders",
                "Source Account Statement",
                "Periodic Payment Report",
                "Scheduled Deposit Collection",
                "Tax Details on Interest & Bonus",
                "Counter Reports",
                "Membership Details",
                "Membership Details with Photo",

                //Analysis
                "Loan Issue and Collection Reports",
                "Ageing Loan Reports",
                "Comprehensive Report Of Loan",
                "Deposit Maturity Reports",
                "Loan Payment Schedule",
                "Pearls Analysis Report",

                //Final Report
                "Branchwise Member Report",
                "Comprehensive Report of Member",
                "Loan Provision Report",
                "NRB Report",
                "Report of Interest",
                "Trial Balance for Internal Audit",
                "Ledger Statement Report",
                "ABBS Statement",
                "Comprehensive Deposit / Loan Report",
            };

            var settlementReportMenu = settlementReport.Select(menuName => new ApplicationMenuDTO
            {
                MenuName = menuName,
                ActionList = new List<Data.Enum.Action>
                            {
                                Data.Enum.Action.View
                            }
            }).ToList();

            this.MenuDTOs = this.MenuDTOs.Concat(settlementReportMenu).ToList();
        }
    }
}