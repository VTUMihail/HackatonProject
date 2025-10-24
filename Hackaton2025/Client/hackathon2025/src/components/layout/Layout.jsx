import { useState } from "react";
import Sidebar from "./Sidebar";
import Navbar from "./Navbar";
import { Outlet } from "react-router-dom";
import React from "react";
export default function Layout() {
  const [open, setOpen] = useState(false);

  return (
    <div className="flex h-screen bg-gray-50">
      {/* Sidebar */}
      <div
        className={`fixed inset-y-0 left-0 z-50 lg:static transform ${
          open ? "translate-x-0" : "-translate-x-full"
        } lg:translate-x-0 transition w-64`}
      >
        <Sidebar />
      </div>
      {open && (
        <div
          className="fixed inset-0 z-40 bg-black opacity-50 lg:hidden"
          onClick={() => setOpen(false)}
        />
      )}
      {/* Main content */}
      <div className="flex-1 flex flex-col">
        <Navbar onMenuClick={() => setOpen(!open)} />
        <main className="flex-1 overflow-y-auto p-6">
          {/* This is where the routed pages will render */}
          <Outlet />
        </main>
      </div>
    </div>
  );
}
