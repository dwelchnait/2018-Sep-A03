using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additional Namespaces
using ChinookSystem.BLL;
using ChinookSystem.ViewModels;
#endregion

namespace WebApp.SamplePages
{
    public partial class SearchByDDL : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MessageLabel.Text = "";
            if(!Page.IsPostBack)
            {
                BindArtistList();
            }
        }

        protected void BindArtistList()
        {
            ArtistController sysmgr = new ArtistController();
            List<SelectionList> info = sysmgr.Artist_List();

            //reminder on how to .Sort in your code behind
            //info.Sort((x, y) => x.DisplayText.CompareTo(y.DisplayText));

            //setup for the ddl
            ArtistList.DataSource = info;
            ArtistList.DataTextField = nameof(SelectionList.DisplayText);
            ArtistList.DataValueField = nameof(SelectionList.ValueId);
            ArtistList.DataBind();

            //setup the prompt
            //ListItem prompt = new ListItem();
            //prompt.Text = "select an artist";
            //prompt.Value = "0";
            //ArtistList.Items.Insert(0, prompt);

            ArtistList.Items.Insert(0, new ListItem("select an artist","0"));
        }
    }
}