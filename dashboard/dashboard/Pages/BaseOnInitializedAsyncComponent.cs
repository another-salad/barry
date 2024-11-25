using Microsoft.AspNetCore.Components;
using Plotly.Blazor.Traces;
using Plotly.Blazor.LayoutLib;
using Plotly.Blazor.Traces.ScatterGlLib;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using mysqlefcore;

namespace dashboard.Pages;

public class BaseDrawChart<TData> : ComponentBase where TData : class {
    public List<TData> sensorData { get; set; }
    public List<object> traces { get; set; }

    protected override async Task OnInitializedAsync() {
        if (sensorData != null) {
            // Determine property name dynamically
            var propertyName = typeof(TData) == typeof(tempCData) ? "temperatureC" : "humidity";

            traces = sensorData
                .GroupBy(r => GetPropertyValue<string>(r, "sensorName"))
                .Select(g => new ScatterGl {
                    X = g.Select(r => (object)GetPropertyValue<DateTime>(r, "timestamp")).ToList(),
                    Y = g.Select(r => (object)GetPropertyValue<double>(r, propertyName)).ToList(),
                    Mode = ModeFlag.Lines,
                    Name = $"Sensor {g.Key}"
                }).ToList<object>();
        }
        await base.OnInitializedAsync();
    }

    private static T GetPropertyValue<T>(object entity, string propertyName) {
        return (T)entity.GetType().GetProperty(propertyName).GetValue(entity);
    }
}
