// src/pages/HomePage.jsx
import React, { useState } from "react";
import { motion, AnimatePresence } from "framer-motion";
import {
  LayoutDashboard,
  Users,
  BookOpen,
  Settings,
  Search,
  Bell,
  User,
  LogOut,
  ChevronLeft,
  ChevronRight,
  Calendar,
  AlertCircle,
  CheckCircle,
  BarChart2,
  PieChart,
} from "lucide-react";

const sidebarVariants = {
  open: { x: 0, opacity: 1, transition: { duration: 0.3 } },
  closed: { x: "-100%", opacity: 0, transition: { duration: 0.3 } },
};

const cardVariants = {
  hidden: { opacity: 0, y: 20 },
  visible: { opacity: 1, y: 0, transition: { duration: 0.5 } },
  hover: {
    scale: 1.05,
    boxShadow: "0 10px 20px rgba(0, 0, 0, 0.1)",
    transition: { duration: 0.2 },
  },
};

const statCardVariants = {
  hidden: { opacity: 0, scale: 0.95 },
  visible: { opacity: 1, scale: 1, transition: { duration: 0.4 } },
  hover: { scale: 1.03, transition: { duration: 0.2 } },
};

const HomePage = () => {

  const classifications = [
    {
      id: 1,
      name: "Undergraduate Theses",
      description: "Select juries for bachelor level projects.",
      juriesSelected: 45,
      totalNeeded: 60,
      progress: 75,
      status: "On Track",
    },
    {
      id: 2,
      name: "Graduate Dissertations",
      description: "Manage jury assignments for master and PhD defenses.",
      juriesSelected: 28,
      totalNeeded: 40,
      progress: 70,
      status: "Pending",
    },
    {
      id: 3,
      name: "Research Projects",
      description: "Assign evaluators for faculty research initiatives.",
      juriesSelected: 15,
      totalNeeded: 25,
      progress: 60,
      status: "Delayed",
    },
    {
      id: 4,
      name: "Capstone Presentations",
      description: "Choose panels for final year student presentations.",
      juriesSelected: 32,
      totalNeeded: 50,
      progress: 64,
      status: "On Track",
    },
    {
      id: 5,
      name: "Post-Doctoral Reviews",
      description: "Evaluate post-doc fellowship applications.",
      juriesSelected: 10,
      totalNeeded: 15,
      progress: 67,
      status: "Pending",
    },
  ];

  const keyStats = [
    {
      title: "Total Classifications",
      value: 5,
      icon: BookOpen,
      color: "text-blue-500",
      bg: "bg-blue-100",
    },
    {
      title: "Juries Assigned",
      value: 130,
      icon: Users,
      color: "text-green-500",
      bg: "bg-green-100",
    },
    {
      title: "Pending Assignments",
      value: 45,
      icon: AlertCircle,
      color: "text-yellow-500",
      bg: "bg-yellow-100",
    },
    {
      title: "Upcoming Deadlines",
      value: 3,
      icon: Calendar,
      color: "text-red-500",
      bg: "bg-red-100",
    },
  ];

  const recentActivities = [
    {
      id: 1,
      description: "Jury assigned to Undergraduate Thesis #123 by Dr. Smith",
      icon: CheckCircle,
      color: "text-green-500",
      time: "2 hours ago",
    },
    {
      id: 2,
      description: "New classification added: Post-Doctoral Reviews",
      icon: BookOpen,
      color: "text-blue-500",
      time: "5 hours ago",
    },
    {
      id: 3,
      description: "Reminder: Jury selection deadline for Graduates in 3 days",
      icon: AlertCircle,
      color: "text-yellow-500",
      time: "1 day ago",
    },
    {
      id: 4,
      description: "Jury conflict resolved for Research Project #45",
      icon: Users,
      color: "text-purple-500",
      time: "2 days ago",
    },
    {
      id: 5,
      description: "System update: New jury matching algorithm implemented",
      icon: Settings,
      color: "text-indigo-500",
      time: "3 days ago",
    },
  ];

  const quickActions = [
    { name: "Add New Classification", icon: BookOpen },
    { name: "Invite New Jury Member", icon: Users },
    { name: "View Reports", icon: BarChart2 },
    { name: "Schedule Meeting", icon: Calendar },
  ];

  return (
    <div className="flex h-screen bg-gradient-to-br from-gray-50 to-gray-200 font-sans overflow-hidden">
    

      <div className="flex-1 flex flex-col overflow-hidden">
       

        <main className="flex-1 p-8 overflow-y-auto bg-gradient-to-br from-gray-50 to-gray-100 space-y-8">

          {/* Key Statistics */}
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6">
            {keyStats.map((stat, index) => (
              <motion.div
                key={stat.title}
                variants={statCardVariants}
                initial="hidden"
                animate="visible"
                whileHover="hover"
                transition={{ delay: index * 0.1 }}
                className={`rounded-2xl shadow-md p-6 cursor-pointer ${stat.bg} flex flex-col items-center text-center`}
              >
                <stat.icon className={`mb-4 ${stat.color}`} size={32} />
                <h4 className="text-xl font-semibold text-gray-800 mb-2">
                  {stat.title}
                </h4>
                <p className="text-3xl font-bold text-gray-900">{stat.value}</p>
              </motion.div>
            ))}
          </div>

          {/* Classifications Grid */}
          <motion.section
            initial={{ opacity: 0, y: 20 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ delay: 0.2, duration: 0.5 }}
          >
            <h3 className="text-2xl font-bold text-gray-800 mb-6 flex items-center">
              <PieChart className="mr-3 text-blue-500" size={28} /> Active
              Classifications
            </h3>
            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
              {classifications.map((classification, index) => (
                <motion.div
                  key={classification.id}
                  variants={cardVariants}
                  initial="hidden"
                  animate="visible"
                  whileHover="hover"
                  transition={{ delay: index * 0.1 }}
                  className="bg-white rounded-2xl shadow-md p-6 cursor-pointer hover:shadow-xl transition"
                >
                  <div className="flex items-center justify-between mb-4">
                    <h4 className="text-xl font-semibold text-gray-800">
                      {classification.name}
                    </h4>
                    <Users className="text-blue-500" size={28} />
                  </div>
                  <p className="text-gray-600 mb-4">
                    {classification.description}
                  </p>
                  <div className="flex justify-between text-sm text-gray-500 mb-2">
                    <span>
                      Selected: {classification.juriesSelected}/
                      {classification.totalNeeded}
                    </span>
                    <span>
                      Status:{" "}
                      <span
                        className={
                          classification.status === "On Track"
                            ? "text-green-500"
                            : classification.status === "Pending"
                            ? "text-yellow-500"
                            : "text-red-500"
                        }
                      >
                        {classification.status}
                      </span>
                    </span>
                  </div>
                  <div className="bg-gray-200 rounded-full h-3 mb-4">
                    <div
                      className="bg-gradient-to-r from-blue-400 to-blue-600 h-3 rounded-full"
                      style={{ width: `${classification.progress}%` }}
                    ></div>
                  </div>
                  <button className="w-full bg-gradient-to-r from-blue-500 to-blue-600 text-white py-3 rounded-lg hover:from-blue-600 hover:to-blue-700 transition font-medium">
                    Manage Juries
                  </button>
                </motion.div>
              ))}
            </div>
          </motion.section>

          {/* Quick Actions */}
          <motion.section
            initial={{ opacity: 0, y: 20 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ delay: 0.4, duration: 0.5 }}
            className="bg-white rounded-2xl shadow-xl p-8"
          >
            <h3 className="text-2xl font-bold text-gray-800 mb-6 flex items-center">
              <BarChart2 className="mr-3 text-green-500" size={28} /> Quick
              Actions
            </h3>
            <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6">
              {quickActions.map((action, index) => (
                <motion.button
                  key={action.name}
                  variants={cardVariants}
                  initial="hidden"
                  animate="visible"
                  whileHover="hover"
                  transition={{ delay: index * 0.1 }}
                  className="bg-gradient-to-br from-gray-50 to-gray-100 rounded-xl shadow-md p-6 flex flex-col items-center justify-center hover:shadow-lg transition"
                >
                  <action.icon className="mb-4 text-blue-500" size={32} />
                  <span className="text-gray-800 font-semibold">
                    {action.name}
                  </span>
                </motion.button>
              ))}
            </div>
          </motion.section>

          {/* Recent Activity */}
          <motion.section
            initial={{ opacity: 0, y: 20 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ delay: 0.6, duration: 0.5 }}
          >
            <h3 className="text-2xl font-bold text-gray-800 mb-6 flex items-center">
              <Calendar className="mr-3 text-purple-500" size={28} /> Recent
              Activity
            </h3>
            <div className="bg-white rounded-2xl shadow-xl overflow-hidden">
              <ul className="divide-y divide-gray-200">
                {recentActivities.map((activity) => (
                  <li
                    key={activity.id}
                    className="p-6 flex items-start space-x-4 hover:bg-gray-50 transition"
                  >
                    <activity.icon
                      className={`mt-1 ${activity.color}`}
                      size={24}
                    />
                    <div className="flex-1">
                      <p className="text-gray-800 font-medium">
                        {activity.description}
                      </p>
                      <p className="text-sm text-gray-500">{activity.time}</p>
                    </div>
                  </li>
                ))}
              </ul>
            </div>
          </motion.section>

          {/* Footer */}
          <footer className="mt-8 text-center text-gray-500 text-sm">
            © 2023 UniJury Dashboard. All rights reserved. | Version 2.1.0
          </footer>
        </main>
      </div>
    </div>
  );
};

export default HomePage;
