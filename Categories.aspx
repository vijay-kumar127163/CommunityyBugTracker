<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Categories.aspx.cs" Inherits="CategoriesPage" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h1>Categories</h1>
    <p><asp:Label ID="MessageLabel" runat="server" EnableViewState="false" /></p>

    <asp:GridView ID="CategoryGrid" runat="server" AutoGenerateColumns="false"
        DataKeyNames="CategoryId"
        OnRowEditing="CategoryGrid_RowEditing"
        OnRowCancelingEdit="CategoryGrid_RowCancelingEdit"
        OnRowUpdating="CategoryGrid_RowUpdating">
        <Columns>
            <asp:TemplateField HeaderText="Name">
                <ItemTemplate><%# Server.HtmlEncode((string)Eval("Name")) %></ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="EditName" runat="server" MaxLength="50" Text='<%# Eval("Name") %>' />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Active">
                <ItemTemplate><%# (bool)Eval("IsActive") ? "Yes" : "No" %></ItemTemplate>
                <EditItemTemplate>
                    <asp:CheckBox ID="EditActive" runat="server" Checked='<%# (bool)Eval("IsActive") %>' />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowEditButton="true" CausesValidation="false" />
        </Columns>
    </asp:GridView>

    <h2>Add a category</h2>
    <p>
        <asp:Label runat="server" AssociatedControlID="NewName" Text="Name:" />
        <asp:TextBox ID="NewName" runat="server" MaxLength="50" />
        <asp:Button ID="AddButton" runat="server" Text="Add" ValidationGroup="Add" OnClick="AddButton_Click" />
        <asp:RequiredFieldValidator runat="server" ControlToValidate="NewName" ValidationGroup="Add"
            CssClass="error" Display="Dynamic" ErrorMessage="Enter a category name." />
    </p>
</asp:Content>
