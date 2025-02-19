﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public enum GanttViewMode
{
    /// <summary>
    /// 
    /// </summary>
    [Description("Quarter Day")]
    QUARTER_DAY,

    /// <summary>
    /// 
    /// </summary>
    [Description("Half Day")]
    HALF_DAY,

    /// <summary>
    /// 
    /// </summary>
    [Description("Day")]
    DAY,

    /// <summary>
    /// 
    /// </summary>
    [Description("Week")]
    WEEK,

    /// <summary>
    /// 
    /// </summary>
    [Description("Month")]
    MONTH,

    /// <summary>
    /// 
    /// </summary>
    [Description("Year")]
    YEAR,
}
