import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Home from "./pages/Home";
import Layout from "./components/layout/Layout";
import React from "react";
import Professors from "./pages/listing/Proffesors";
import { JuryGenerator, JuryHistory } from "./pages/JuryGenerator";
export default function App() {
  return (
    <Router>
      <Routes>
        {/* Auth pages (no Layout) */}

        {/* Dashboard pages (with Layout) */}
        <Route element={<Layout />}>
          <Route path="/" element={<Home />} />
          <Route path="/professors" element={<Professors />} />
          <Route path="/generator" element={<JuryGenerator />} />
          <Route path="/history" element={<JuryHistory />} />
        </Route>
      </Routes>
    </Router>
  );
}
