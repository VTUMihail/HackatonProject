import React, { useState } from "react";
import { useParams } from "react-router-dom";
import { ArrowLeft, Edit3, Trash2 } from "lucide-react";

// Sample data for testing - extending the professor with jury history
const sampleProfessors = [
  {
    _id: "prof1",
    fullName: "Dr. John Smith",
    photo: "https://randomuser.me/api/portraits/men/1.jpg",
    address: "123 University Ave, Boston, MA",
    phoneNumber: "+1 (555) 123-4567",
    faculty: "Math and IT",
    yearsOfWork: 15,
    specialties: ["Artificial Intelligence", "Data Science", "Cryptography"],
    juryHistory: [
      {
        id: "jury1",
        event: "International AI Conference 2023",
        role: "Panel Judge",
        date: "June 15, 2023",
        location: "San Francisco, CA",
        description:
          "Evaluated research papers on machine learning advancements.",
      },
      {
        id: "jury2",
        event: "Data Science Symposium 2022",
        role: "Lead Juror",
        date: "April 10, 2022",
        location: "New York, NY",
        description: "Assessed projects in big data analytics and AI ethics.",
      },
      {
        id: "jury3",
        event: "Cryptography Workshop 2021",
        role: "Expert Reviewer",
        date: "September 5, 2021",
        location: "Boston, MA",
        description: "Reviewed submissions on quantum-resistant encryption.",
      },
    ],
  },
  // Add more professors if needed...
];

export default function ProfessorDetail() {
  const { id } = useParams();
  const [activeTab, setActiveTab] = useState("overview");

  // Fetch professor data (using sample for testing)
  const professor = sampleProfessors.find((p) => p._id === id);

  if (!professor) {
    return <p>Professor not found.</p>;
  }

  return (
    <div className="max-w-4xl mx-auto space-y-8 p-6 bg-white rounded-xl shadow-lg">
      {/* Header */}
      <div className="flex items-center justify-between">
        <div className="flex items-center gap-4">
          <button
            onClick={() => window.history.back()}
            className="p-2 rounded-full hover:bg-gray-100"
          >
            <ArrowLeft size={24} className="text-gray-600" />
          </button>
          <h1 className="text-3xl font-bold text-gray-800">
            {professor.fullName}
          </h1>
        </div>
        <div className="flex gap-2">
          <button
            className="p-2 rounded-full hover:bg-gray-100"
            onClick={() => alert(`Edit ${professor.fullName}`)}
          >
            <Edit3 size={20} className="text-gray-600" />
          </button>
          <button
            className="p-2 rounded-full hover:bg-gray-100"
            onClick={() => alert(`Delete ${professor.fullName}`)}
          >
            <Trash2 size={20} className="text-red-500" />
          </button>
        </div>
      </div>

      {/* Tabs */}
      <div className="border-b border-gray-200">
        <nav className="-mb-px flex space-x-8">
          <button
            onClick={() => setActiveTab("overview")}
            className={`py-4 px-1 border-b-2 font-medium text-sm ${
              activeTab === "overview"
                ? "border-blue-600 text-blue-600"
                : "border-transparent text-gray-500 hover:text-gray-700 hover:border-gray-300"
            }`}
          >
            Overview
          </button>
          <button
            onClick={() => setActiveTab("jury")}
            className={`py-4 px-1 border-b-2 font-medium text-sm ${
              activeTab === "jury"
                ? "border-blue-600 text-blue-600"
                : "border-transparent text-gray-500 hover:text-gray-700 hover:border-gray-300"
            }`}
          >
            Jury History
          </button>
        </nav>
      </div>

      {/* Tab Content */}
      {activeTab === "overview" && (
        <div className="space-y-6">
          <div className="flex gap-6">
            <img
              src={professor.photo}
              alt={professor.fullName}
              className="w-32 h-32 rounded-full object-cover shadow-md"
            />
            <div className="flex-1 space-y-4">
              <div>
                <h3 className="text-lg font-semibold text-gray-700">Faculty</h3>
                <p className="text-gray-600">{professor.faculty}</p>
              </div>
              <div>
                <h3 className="text-lg font-semibold text-gray-700">
                  Years of Work
                </h3>
                <p className="text-gray-600">{professor.yearsOfWork} years</p>
              </div>
            </div>
            <div className="flex-1 space-y-4">
              <div>
                <h3 className="text-lg font-semibold text-gray-700">Address</h3>
                <p className="text-gray-600">{professor.address}</p>
              </div>
              <div>
                <h3 className="text-lg font-semibold text-gray-700">
                  Phone Number
                </h3>
                <p className="text-gray-600">{professor.phoneNumber}</p>
              </div>
            </div>
          </div>

          <div>
            <h3 className="text-lg font-semibold text-gray-700 mb-2">
              Specialties
            </h3>
            <div className="flex flex-wrap gap-2">
              {professor.specialties.map((specialty, i) => (
                <span
                  key={i}
                  className="px-3 py-1 rounded-full bg-blue-50 text-blue-600 text-sm"
                >
                  {specialty}
                </span>
              ))}
            </div>
          </div>
        </div>
      )}

      {activeTab === "jury" && (
        <div className="space-y-6">
          {professor.juryHistory.length > 0 ? (
            professor.juryHistory.map((jury) => (
              <div
                key={jury.id}
                className="p-4 border border-gray-200 rounded-lg space-y-2"
              >
                <div className="flex justify-between items-center">
                  <h3 className="text-lg font-semibold text-gray-800">
                    {jury.event}
                  </h3>
                  <span className="text-sm text-gray-500">{jury.date}</span>
                </div>
                <p className="text-gray-600">{jury.description}</p>
                <div className="flex gap-4 text-sm text-gray-500">
                  <span>Role: {jury.role}</span>
                  <span>Location: {jury.location}</span>
                </div>
              </div>
            ))
          ) : (
            <p className="text-gray-600">No jury history available.</p>
          )}
        </div>
      )}
    </div>
  );
}
