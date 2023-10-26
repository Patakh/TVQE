using System;
using System.Collections.Generic;

namespace TVQE.Context;

/// <summary>
/// Текст на табло
/// </summary>
public partial class SOfficeScoreboardText
{
    /// <summary>
    /// Ключ
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Табло
    /// </summary>
    public long SOfficeScoreboardId { get; set; }

    /// <summary>
    /// Текст для монитора
    /// </summary>
    public string TextMonitor { get; set; } = null!;

    /// <summary>
    /// Активность
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Кто добавил
    /// </summary>
    public string EmployeeNameAdd { get; set; } = null!;

    /// <summary>
    /// Дата и время добавления
    /// </summary>
    public DateTime? DateAdd { get; set; }
}
