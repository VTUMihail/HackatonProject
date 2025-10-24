import { Home, List, LogOut, UsersIcon,ChevronDown, ChevronUp, CaravanIcon, TentIcon, ChartBar, Mail, Star } from "lucide-react";
import { NavLink } from "react-router-dom";
import React, { useState } from "react";

const links = [
  { to: "/", label: "Home", icon: <Home size={20} /> },
  { to: "/generator", label: "Generate Jury", icon: <Home size={20} /> },

  {
    label: "Listings",
    icon: <List size={20} />,
    subLinks: [
      { to: "/professors", label: "Professors", icon: <UsersIcon size={20} /> },
      {
        to: "/universities",
        label: "Universities",
        icon: <UsersIcon size={20} />,
      },
    ],
  },
];
export default function Sidebar() {
    const [isListingsOpen, setIsListingsOpen] = useState(false);

  return (
    <aside className="w-64 h-screen bg-white border-r border-gray-200 p-4 flex flex-col">
      <h1 className="text-2xl font-bold mb-6">Dashboard</h1>
      <nav className="flex-1">
        <ul className="list-none">
          {links.map(({ to, label, icon, subLinks }) => (
            <li key={to || label}>
              {subLinks ? (
                <>
                  <button
                    onClick={() => setIsListingsOpen(!isListingsOpen)}
                    className="flex items-center gap-2 px-3 py-2 rounded-xl text-gray-700 hover:bg-gray-100 w-full transition"
                  >
                    {icon} {label}
                    {isListingsOpen ? <ChevronUp size={20} /> : <ChevronDown size={20} />}
                  </button>
                  {isListingsOpen && (
                    <ul className="ml-6 mt-2 space-y-2 list-none">
                      {subLinks.map(({ to: subTo, label: subLabel,icon:subIcon }) => (
                        <li key={subTo}>
                          <NavLink
                            to={subTo}
                            className={({ isActive }) =>
                              `flex items-center gap-2 px-3 py-2 rounded-xl transition ${
                                isActive ? "bg-blue-600 text-white" : "text-gray-700 hover:bg-gray-100"
                              }`
                            }
                          >
                            {subIcon} {subLabel}
                          </NavLink>
                        </li>
                      ))}
                    </ul>
                  )}
                </>
              ) : (
                <NavLink
                  to={to}
                  className={({ isActive }) =>
                    `flex items-center gap-2 px-3 py-2 rounded-xl transition ${
                      isActive ? "bg-blue-600 text-white" : "text-gray-700 hover:bg-gray-100"
                    }`
                  }
                >
                  {icon} {label}
                </NavLink>
              )}
            </li>
          ))}
        </ul>
      </nav>
      <button className="flex items-center gap-2 text-gray-600 hover:text-red-600">
        <LogOut size={20} /> Logout
      </button>
    </aside>
  );
}
