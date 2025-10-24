import React, { useState } from "react";
import { useParams } from "react-router-dom";
import { ArrowLeft, Edit3, Trash2 } from "lucide-react";

const sampleUniversities = [
  {
    _id: "uni1",
    title: "Massachusetts Institute of Technology",
    photo: "https://randomuser.me/api/portraits/lego/1.jpg",
    address: "77 Massachusetts Ave, Cambridge, MA 02139",
    phoneNumber: "+1 (617) 253-1000",
    faculties: [
      "Engineering",
      "Computer Science",
      "Physics",
      "Mathematics",
      "Business",
    ],
    establishedYear: 1861,
  },
  {
    _id: "uni2",
    title: "Stanford University",
    photo: "https://randomuser.me/api/portraits/lego/2.jpg",
    address: "450 Serra Mall, Stanford, CA 94305",
    phoneNumber: "+1 (650) 723-2300",
    faculties: ["Engineering", "Medicine", "Law", "Education"],
    establishedYear: 1885,
  },
  {
    _id: "uni3",
    title: "Harvard University",
    photo: "https://randomuser.me/api/portraits/lego/3.jpg",
    address: "Cambridge, MA 02138",
    phoneNumber: "+1 (617) 495-1000",
    faculties: [
      "Arts and Sciences",
      "Medicine",
      "Law",
      "Business",
      "Education",
      "Engineering",
    ],
    establishedYear: 1636,
  },
];

export default function UniversityDetail() {
  const { id } = useParams();
  const [activeTab, setActiveTab] = useState("overview");

  const university = sampleUniversities.find((u) => u._id === id);

  if (!university) {
    return (
      <div className="max-w-4xl mx-auto p-6 text-center">
        <p className="text-gray-600">University not found.</p>
      </div>
    );
  }

  return (
    <div className="max-w-5xl mx-auto space-y-8 p-6 bg-white rounded-xl shadow-lg">
      <div className="flex items-center justify-between">
        <div className="flex items-center gap-4">
          <button
            onClick={() => window.history.back()}
            className="p-2 rounded-full hover:bg-gray-100 transition-colors"
          >
            <ArrowLeft size={24} className="text-gray-600" />
          </button>
          <h1 className="text-3xl font-bold text-gray-800">
            {university.title}
          </h1>
        </div>
        <div className="flex gap-2">
          <button
            className="p-2 rounded-full hover:bg-gray-100 transition-colors"
            onClick={() => alert(`Edit ${university.title}`)}
          >
            <Edit3 size={20} className="text-gray-600" />
          </button>
          <button
            className="p-2 rounded-full hover:bg-gray-100 transition-colors"
            onClick={() => alert(`Delete ${university.title}`)}
          >
            <Trash2 size={20} className="text-red-500" />
          </button>
        </div>
      </div>

      <div className="border-b border-gray-200">
        <nav className="-mb-px flex space-x-8">
          <button
            onClick={() => setActiveTab("overview")}
            className={`py-4 px-1 border-b-2 font-medium text-sm transition-colors ${
              activeTab === "overview"
                ? "border-blue-600 text-blue-600"
                : "border-transparent text-gray-500 hover:text-gray-700 hover:border-gray-300"
            }`}
          >
            Overview
          </button>
        </nav>
      </div>

      {activeTab === "overview" && (
        <div className="space-y-8">
          {/* Faculties */}
          <div>
            <h3 className="text-lg font-semibold text-gray-700 mb-3">
              Faculties
            </h3>
            <div className="flex flex-wrap gap-2">
              {university.faculties.map((faculty, i) => (
                <span
                  key={i}
                  className="px-3 py-1.5 rounded-full bg-green-50 text-green-700 text-sm border border-green-200"
                >
                  {faculty}
                </span>
              ))}
            </div>
          </div>
        </div>
      )}

    </div>
  );
}
