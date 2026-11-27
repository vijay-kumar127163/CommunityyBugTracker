<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="DefaultPage" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h1>Category summary</h1>
    <asp:GridView ID="SummaryGrid" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="Category" HeaderText="Category" />
            <asp:BoundField DataField="OpenCount" HeaderText="Open" />
            <asp:BoundField DataField="InProgressCount" HeaderText="In Progress" />
            <asp:BoundField DataField="ResolvedCount" HeaderText="Resolved" />
            <asp:BoundField DataField="ClosedCount" HeaderText="Closed" />
            <asp:BoundField DataField="TotalCount" HeaderText="Total" />
        </Columns>
    </asp:GridView>
    <p><asp:Label ID="TotalLabel" runat="server" /></p>

    <h2>Issues by status</h2>
    <p>
        <asp:Label runat="server" AssociatedControlID="StatusList" Text="Status:" />
        <asp:DropDownList ID="StatusList" runat="server" AutoPostBack="true"
            OnSelectedIndexChanged="StatusList_SelectedIndexChanged">
            <asp:ListItem>Open</asp:ListItem>
            <asp:ListItem>In Progress</asp:ListItem>
            <asp:ListItem>Resolved</asp:ListItem>
            <asp:ListItem>Closed</asp:ListItem>
        </asp:DropDownList>
    </p>
    <asp:GridView ID="IssueGrid" runat="server" AutoGenerateColumns="false"
        EmptyDataText="No issues with this status.">
        <Columns>
            <asp:BoundField DataField="IssueId" HeaderText="#" />
            <asp:BoundField DataField="Title" HeaderText="Title" />
            <asp:BoundField DataField="Priority" HeaderText="Priority" />
            <asp:BoundField DataField="ReporterName" HeaderText="Reported by" />
            <asp:BoundField DataField="CreatedAt" HeaderText="Date" DataFormatString="{0:dd MMM yyyy}" />
        </Columns>
    </asp:GridView>
</asp:Content>
