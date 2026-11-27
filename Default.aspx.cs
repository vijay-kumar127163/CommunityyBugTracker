using System;
using System.Data;
using System.Data.SqlClient;

public partial class DefaultPage : AdminPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // Bind only on the first visit; ViewState keeps the data across postbacks
        if (!IsPostBack)
        {
            BindSummary();
            BindIssues();
        }
    }

    protected void StatusList_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindIssues();
    }

    private void BindSummary()
    {
        DataTable summary = Db.Query("dbo.usp_CategorySummary", CommandType.StoredProcedure);
        SummaryGrid.DataSource = summary;
        SummaryGrid.DataBind();

        int total = 0;
        foreach (DataRow row in summary.Rows)
        {
            total += Convert.ToInt32(row["TotalCount"]);
        }
        TotalLabel.Text = "Total issues: " + total;
    }

    private void BindIssues()
    {
        IssueGrid.DataSource = Db.Query(
            @"SELECT i.IssueId, i.Title, i.Priority, u.FullName AS ReporterName, i.CreatedAt
              FROM Issues i
              INNER JOIN Users u ON u.UserId = i.ReportedBy
              WHERE i.Status = @Status
              ORDER BY i.CreatedAt DESC",
            CommandType.Text,
            new SqlParameter("@Status", StatusList.SelectedValue));
        IssueGrid.DataBind();
    }
}
