using System;
using System.Web.UI;

/// <summary>
/// Base class for every admin page. The module has no login of its own,
/// so it only answers requests made on the same computer.
/// </summary>
public class AdminPage : Page
{
    protected override void OnInit(EventArgs e)
    {
        if (!Request.IsLocal)
        {
            Response.StatusCode = 403;
            Response.End();
        }

        // Ties ViewState to this browser session (protects against forged postbacks)
        Session["Active"] = true;
        ViewStateUserKey = Session.SessionID;

        base.OnInit(e);
    }
}
