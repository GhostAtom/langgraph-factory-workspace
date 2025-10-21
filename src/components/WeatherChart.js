import React from 'react';
import { Line } from 'react-chartjs-2';

const WeatherChart = () => {
  const data = {
    labels: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri'],
    datasets: [
      {
        label: 'Wind Speed (knots)',
        data: [12, 19, 13, 15, 22],
        fill: false,
        backgroundColor: 'rgba(75,192,192,0.4)',
        borderColor: 'rgba(75,192,192,1)'
      }
    ]
  };

  return (
    <div className="weather-chart">
      <Line data={data} />
    </div>
  );
};

export default WeatherChart;
