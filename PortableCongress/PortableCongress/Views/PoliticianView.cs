#pragma warning disable 1591
// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.17020
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace PortableCongress
{
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


[System.CodeDom.Compiler.GeneratedCodeAttribute("RazorTemplatePreprocessor", "2.6.0.0")]
public partial class PoliticianView : PortableRazor.ViewBase
{

#line hidden

#line 2 "PoliticianView.cshtml"
public PortableCongress.Politician Model { get; set; }

#line default
#line hidden


public override void Execute()
{
WriteLiteral("<html>\n<head>\n\t<link");

WriteLiteral(" rel=\"stylesheet\"");

WriteLiteral(" href=\"jquery.mobile-1.4.0.min.css\"");

WriteLiteral(" />\n\t<script");

WriteLiteral(" src=\"jquery-1.10.2.min.js\"");

WriteLiteral("></script>\n\t<script");

WriteLiteral(" src=\"jquery.mobile-1.4.0.min.js\"");

WriteLiteral("></script>\n</head>\n<body>\n\t<div");

WriteLiteral(" data-role=\"header\"");

WriteLiteral(" style=\"overflow:hidden;\"");

WriteLiteral(" data-position=\"fixed\"");

WriteLiteral(">\n");

WriteLiteral("\t\t");


#line 12 "PoliticianView.cshtml"
Write(Html.ActionLink("Back", "ShowPoliticianList", null, new { 
			@class="ui-btn-left ui-btn ui-icon-back ui-btn-icon-notext ui-shadow ui-corner-all",
			data_icon = "arrow-l", 
			data_role="button", 
			data_mini="true", 
			data_inline="true", 
			data_transition="slide"}));


#line default
#line hidden
WriteLiteral("\n    \t<h1>");


#line 19 "PoliticianView.cshtml"
    Write(Model.FirstName);


#line default
#line hidden
WriteLiteral(" ");


#line 19 "PoliticianView.cshtml"
                     Write(Model.LastName);


#line default
#line hidden
WriteLiteral("</h1>\n    </div>\n\t\t\n    <div");

WriteLiteral(" style=\"margin-left:10\"");

WriteLiteral(" data-mini=\"true\"");

WriteLiteral(" data-inset=\"false\"");

WriteLiteral(">\n\t    <h4>");


#line 23 "PoliticianView.cshtml"
    Write(Model.FirstName);


#line default
#line hidden
WriteLiteral(" ");


#line 23 "PoliticianView.cshtml"
                     Write(Model.LastName);


#line default
#line hidden
WriteLiteral(" - ");


#line 23 "PoliticianView.cshtml"
                                       Write(Model.Party);


#line default
#line hidden
WriteLiteral(" ");


#line 23 "PoliticianView.cshtml"
                                                    Write(Model.State);


#line default
#line hidden
WriteLiteral("</h4>\n\t    <p><img");

WriteAttribute ("src", " src=\'", "\'"

#line 24 "PoliticianView.cshtml"
, Tuple.Create<string,object,bool> ("", Model.ImageName

#line default
#line hidden
, false)
);
WriteLiteral("/></p>\n\t    <p>");


#line 25 "PoliticianView.cshtml"
   Write(Model.Phone);


#line default
#line hidden
WriteLiteral("</p>\n\t\t<p>");


#line 26 "PoliticianView.cshtml"
Write(Model.OfficeAddress);


#line default
#line hidden
WriteLiteral("</p>\n\t</div>\n\n\t<div");

WriteLiteral(" style=\"margin-left:10; margin-right:10\"");

WriteLiteral(" data-role=\"collapsible\"");

WriteLiteral(" data-mini=\"true\"");

WriteLiteral(" data-collapsed=\"false\"");

WriteLiteral(" data-inset=\"false\"");

WriteLiteral(">\n\t    <h4>More Info</h4>\n\t\t<ul");

WriteLiteral(" data-role=\"listview\"");

WriteLiteral(" data-inset=\"false\"");

WriteLiteral(">\n\t\t\t<li>");


#line 32 "PoliticianView.cshtml"
  Write(Html.ActionLink("Recent Votes", "ShowRecentVotes", new {id = @Model.Id }));


#line default
#line hidden
WriteLiteral("</li>\n\t\t\t<li>");


#line 33 "PoliticianView.cshtml"
  Write(Html.ActionLink("Committees", "ShowCommittees", new {id = @Model.Id, bioguideid = @Model.BioGuideId }));


#line default
#line hidden
WriteLiteral("</li>\n\t\t</ul> \n\t</div>\n\n</body>\n</html>\n");

}
}
}
#pragma warning restore 1591
