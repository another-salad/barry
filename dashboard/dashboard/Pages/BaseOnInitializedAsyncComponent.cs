using Microsoft.AspNetCore.Components;
using Plotly.Blazor.Traces;
using Plotly.Blazor.LayoutLib;
using Plotly.Blazor.Traces.ScatterGlLib;

namespace dashboard.Pages;

public class BaseDrawChart : ComponentBase {

    public List<mysqlefcore.tempCData> ?sensorData;

    public List<object> ?traces;

    protected override async Task OnInitializedAsync() {
        if (sensorData != null) {
            traces = sensorData.GroupBy(r => r.sensorName)
                                .Select(g => new ScatterGl(){
                                    X = g.Select(r => (object)r.timestamp).ToList(),
                                    Y = g.Select(r => (object)r.temperatureC).ToList(),
                                    Mode = ModeFlag.Lines,
                                    Name = $"Sensor {g.Key}"
                                }).ToList<object>();
        }
        await base.OnInitializedAsync();
    }

}
