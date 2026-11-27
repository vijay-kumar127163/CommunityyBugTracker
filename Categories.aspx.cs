using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class CategoriesPage : AdminPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid();
        }
    }

    protected void AddButton_Click(object sender, EventArgs e)
    {
        Page.Validate("Add");
        string name = NewName.Text.Trim();
        if (!Page.IsValid || !IsValidName(name)) return;

        if (Save("INSERT INTO Categories (Name) VALUES (@Name)",
                 new SqlParameter("@Name", name)))
        {
            NewName.Text = "";
            MessageLabel.Text = "Category added.";
            BindGrid();
        }
    }

    protected void CategoryGrid_RowEditing(object sender, GridViewEditEventArgs e)
    {
        CategoryGrid.EditIndex = e.NewEditIndex;
        BindGrid();
    }

    protected void CategoryGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        CategoryGrid.EditIndex = -1;
        BindGrid();
    }

    protected void CategoryGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = CategoryGrid.Rows[e.RowIndex];
        string name = ((TextBox)row.FindControl("EditName")).Text.Trim();
        bool active = ((CheckBox)row.FindControl("EditActive")).Checked;
        int id = (int)CategoryGrid.DataKeys[e.RowIndex].Value;

        if (!IsValidName(name)) return;

        if (Save("UPDATE Categories SET Name = @Name, IsActive = @IsActive WHERE CategoryId = @Id",
                 new SqlParameter("@Name", name),
                 new SqlParameter("@IsActive", active),
                 new SqlParameter("@Id", id)))
        {
            CategoryGrid.EditIndex = -1;
            MessageLabel.Text = "Category updated.";
            BindGrid();
        }
    }

    private bool IsValidName(string name)
    {
        if (name.Length >= 1 && name.Length <= 50) return true;
        MessageLabel.Text = "A category name must be 1 to 50 characters.";
        return false;
    }

    private bool Save(string sql, params SqlParameter[] parameters)
    {
        try
        {
            Db.Execute(sql, parameters);
            return true;
        }
        catch (SqlException ex)
        {
            if (ex.Number != 2627 && ex.Number != 2601) throw;
            MessageLabel.Text = "A category with that name already exists.";
            return false;
        }
    }

    private void BindGrid()
    {
        CategoryGrid.DataSource = Db.Query(
            "SELECT CategoryId, Name, IsActive FROM Categories ORDER BY Name", CommandType.Text);
        CategoryGrid.DataBind();
    }
}
