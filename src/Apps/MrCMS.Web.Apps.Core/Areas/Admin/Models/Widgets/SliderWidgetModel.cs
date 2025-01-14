﻿using System.Collections.Generic;
using System.ComponentModel;
using MrCMS.Web.Admin.Infrastructure.ModelBinding;
using MrCMS.Web.Apps.Core.Models.Widgets;
using MrCMS.Web.Apps.Core.Widgets;
using Newtonsoft.Json;

namespace MrCMS.Web.Apps.Core.Areas.Admin.Models.Widgets;

public class SliderWidgetModel : IUpdatePropertiesViewModel<SliderWidget>, IAddPropertiesViewModel<SliderWidget>
{
    public int Id { get; set; }
    public List<SlideViewModel> SlideList { get; set; }
    
    public int Interval { get; set; } = 5000;

    public bool ShowIndicator { get; set; } = true;

    public bool PauseOnHover { get; set; }
    
    [DisplayName("Caption Css Class")]
    public string CaptionCssClass { get; set; } = "d-none d-md-block";
    
    public string BackgroundColor { get; set; } = "transparent";
    
    public string DesktopSlideRatio { get; set; } = "19x6";
    public string MobileSlideRatio { get; set; } = "1x1";

    public override string ToString()
    {
        return JsonConvert.SerializeObject((SlideList ?? new List<SlideViewModel>()));
    }
}