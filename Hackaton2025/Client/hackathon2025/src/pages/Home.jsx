/* eslint-disable no-unused-vars */
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
import RecentActivityCard from "../components/ui/cards/homePage/RecentActivityCard";
import QuickActionCard from "../components/ui/cards/homePage/QuickActionCard";
import { useGetTeachers } from "../hooks/teachers";
import { path } from "framer-motion/client";

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
  const {data:dbTeachers} = useGetTeachers();


  const keyStats = [
    {
      title: "Total Teachers",
      value: dbTeachers?.length? dbTeachers?.length : 0,
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
      title: "Total Procedures",
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
    { name: "Add New Teacher", icon: BookOpen,path:"/teachers/addNewTeacher" },
    { name: "Generate New Jury", icon: Users, path:"/generator" },
    { name: "View Reports", icon: BarChart2, path:"/history" },
    { name: "Schedule Meeting", icon: Calendar },
  ];
  const {data:teachers} = useGetTeachers();
  console.log(teachers,"teachers");
  

  return (
    <div className="flex h-screen bg-linear-to-br from-gray-50 to-gray-200 font-sans overflow-hidden">
    

       

        <main className="flex-1 p-8 overflow-y-auto bg-lsinear-to-br from-gray-50 to-gray-100 space-y-8">

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
               <QuickActionCard action={action} index={index} cardVariants={cardVariants}/>
              ))}
            </div>
          </motion.section>

          {/* Recent Activity */}
          {/* <motion.section
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
                  <RecentActivityCard activity={activity}/>
                ))}
              </ul>
            </div>
          </motion.section> */}

          {/* Footer */}
          <footer className="mt-8 text-center text-gray-500 text-sm">
            © 2025 UniJury Dashboard. All rights reserved.
          </footer>
        </main>
    </div>
  );
};

export default HomePage;
