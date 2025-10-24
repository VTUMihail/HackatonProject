/* eslint-disable no-unused-vars */
import React from "react";
import { motion, AnimatePresence } from "framer-motion";
import { useNavigate } from "react-router-dom";

export default function QuickActionCard({ action,cardVariants,index }) {
  const navigate = useNavigate();
  return (
    <motion.button
      key={action.name}
      variants={cardVariants}
      initial="hidden"
      animate="visible"
      onClick={()=>{navigate(action?.path)}}
      whileHover="hover"
      transition={{ delay: index * 0.1 }}
      className="bg-linear-to-br hover:cursor-pointer from-gray-50 to-gray-100 rounded-xl shadow-md p-6 flex flex-col items-center justify-center hover:shadow-lg transition"
    >
      <action.icon className="mb-4 text-blue-500" size={32} />
      <span className="text-gray-800 font-semibold">{action.name}</span>
    </motion.button>
  );
}
