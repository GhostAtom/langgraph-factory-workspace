import React, { useState, useEffect } from 'react';
import axios from 'axios';
import WindDirection from './WindDirection';
import WeatherChart from './WeatherChart';

const WeatherDashboard = () => {
  const [windData, setWindData] = useState({ speed: 0, direction: 'N/A' });

  useEffect(() => {
    const fetchWeatherData = async () => {
      try {
        const response = await axios.get(`${process.env.REACT_APP_API_URL}/weather`, {
          headers: { 'Authorization': `Bearer ${process.env.REACT_APP_API_KEY}` }
        });
        const { speed, direction } = response.data;
        setWindData({ speed, direction });
      } catch (error) {
        console.error("Error fetching weather data: ", error);
      }
    };

    fetchWeatherData();
  }, []);

  return (
    <div className="weather-dashboard">
      <h1>Marine Weather Dashboard</h1>
      <p>Wind Speed: {windData.speed} knots</p>
      <WindDirection direction={windData.direction} />
      <WeatherChart />
    </div>
  );
};

export default WeatherDashboard;
