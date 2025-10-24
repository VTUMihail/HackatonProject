import React, { useState, useEffect } from "react";
import { Users, RefreshCw, Calendar, Clock, User } from "lucide-react";
import { motion, AnimatePresence } from "framer-motion";

// Sample professors data
const professors = [
  {
    id: "prof1",
    fullName: "Dr. John Smith",
    photo: "https://randomuser.me/api/portraits/men/1.jpg",
    address: "123 University Ave, Boston, MA",
    phoneNumber: "+1 (555) 123-4567",
    faculty: "Math and IT",
    yearsOfWork: 15,
    specialties: ["Artificial Intelligence", "Data Science", "Cryptography"],
  },
  {
    id: "prof2",
    fullName: "Dr. Emily Johnson",
    photo: "https://randomuser.me/api/portraits/women/2.jpg",
    address: "456 College St, Cambridge, MA",
    phoneNumber: "+1 (555) 987-6543",
    faculty: "Physics",
    yearsOfWork: 8,
    specialties: ["Quantum Mechanics", "Astrophysics"],
  },
  {
    id: "prof3",
    fullName: "Dr. Michael Brown",
    photo: "https://randomuser.me/api/portraits/men/3.jpg",
    address: "789 Academic Rd, New York, NY",
    phoneNumber: "+1 (555) 456-7890",
    faculty: "Math and IT",
    yearsOfWork: 20,
    specialties: ["Software Engineering", "Algorithms"],
  },
  {
    id: "prof4",
    fullName: "Dr. Sarah Davis",
    photo: "https://randomuser.me/api/portraits/women/4.jpg",
    address: "101 Research Blvd, Stanford, CA",
    phoneNumber: "+1 (555) 321-0987",
    faculty: "Biology",
    yearsOfWork: 12,
    specialties: ["Genetics", "Molecular Biology"],
  },
  {
    id: "prof5",
    fullName: "Dr. David Wilson",
    photo: "https://randomuser.me/api/portraits/men/5.jpg",
    address: "202 Innovation Way, Chicago, IL",
    phoneNumber: "+1 (555) 654-3210",
    faculty: "Chemistry",
    yearsOfWork: 18,
    specialties: ["Organic Chemistry", "Nanotechnology"],
  },
  {
    id: "prof6",
    fullName: "Dr. Jessica Martinez",
    photo: "https://randomuser.me/api/portraits/women/6.jpg",
    address: "303 Knowledge Ln, Austin, TX",
    phoneNumber: "+1 (555) 789-0123",
    faculty: "Engineering",
    yearsOfWork: 10,
    specialties: ["Mechanical Engineering", "Robotics"],
  },
  {
    id: "prof7",
    fullName: "Dr. Robert Lee",
    photo: "https://randomuser.me/api/portraits/men/7.jpg",
    address: "404 Scholar Dr, Seattle, WA",
    phoneNumber: "+1 (555) 210-9876",
    faculty: "Math and IT",
    yearsOfWork: 22,
    specialties: ["Machine Learning", "Big Data"],
  },
  {
    id: "prof8",
    fullName: "Dr. Linda Garcia",
    photo: "https://randomuser.me/api/portraits/women/8.jpg",
    address: "505 Wisdom St, Miami, FL",
    phoneNumber: "+1 (555) 543-2109",
    faculty: "Psychology",
    yearsOfWork: 14,
    specialties: ["Cognitive Psychology", "Behavioral Science"],
  },
  {
    id: "prof9",
    fullName: "Dr. Thomas Rodriguez",
    photo: "https://randomuser.me/api/portraits/men/9.jpg",
    address: "606 Education Ave, Denver, CO",
    phoneNumber: "+1 (555) 876-5432",
    faculty: "History",
    yearsOfWork: 16,
    specialties: ["Ancient History", "Archaeology"],
  },
  {
    id: "prof10",
    fullName: "Dr. Patricia Hernandez",
    photo: "https://randomuser.me/api/portraits/women/10.jpg",
    address: "707 Learning Blvd, Phoenix, AZ",
    phoneNumber: "+1 (555) 109-8765",
    faculty: "Economics",
    yearsOfWork: 11,
    specialties: ["Macroeconomics", "International Trade"],
  },
  {
    id: "prof11",
    fullName: "Dr. Christopher Lopez",
    photo: "https://randomuser.me/api/portraits/men/11.jpg",
    address: "808 Campus Rd, Atlanta, GA",
    phoneNumber: "+1 (555) 432-1098",
    faculty: "Math and IT",
    yearsOfWork: 13,
    specialties: ["Cybersecurity", "Network Systems"],
  },
  {
    id: "prof12",
    fullName: "Dr. Barbara Gonzalez",
    photo: "https://randomuser.me/api/portraits/women/12.jpg",
    address: "909 Academy Ln, Portland, OR",
    phoneNumber: "+1 (555) 765-4321",
    faculty: "Literature",
    yearsOfWork: 9,
    specialties: ["Modern Literature", "Poetry"],
  },
];

const slotVariants = {
  enter: { y: -400, opacity: 0, scale: 0.8 },
  center: {
    y: 0,
    opacity: 1,
    scale: 1,
    transition: { duration: 0.3, ease: "easeOut" },
  },
  exit: {
    y: 400,
    opacity: 0,
    scale: 1.2,
    transition: { duration: 0.3, ease: "easeIn" },
  },
};

export function JuryGenerator() {
  const [jury, setJury] = useState([
    undefined,
    undefined,
    undefined,
    undefined,
  ]);
  const [isGenerating, setIsGenerating] = useState(false);

  const generateAnimationSequence = (target) => {
    const sequence = [];
    // Create a sequence that cycles through all professors multiple times
    for (let i = 0; i < 3; i++) {
      // 3 full cycles through all professors
      const cycleShuffled = [...professors].sort(() => Math.random() - 0.5);
      sequence.push(...cycleShuffled);
    }
    if (sequence.length > 0) {
      sequence.pop(); // Remove last to avoid potential duplicate transition
    }
    sequence.push(target); // Final professor
    return sequence;
  };

  const animateSlot = async (slotIndex, target) => {
    const sequence = generateAnimationSequence(target);
    for (let i = 0; i < sequence.length; i++) {
      setJury((prev) => {
        const newJury = [...prev];
        newJury[slotIndex] = sequence[i];
        return newJury;
      });
      const progress = i / sequence.length;
      // Slower animation: 300ms at start, increasing to 600ms towards the end
      const speed = 300 + progress * 300;
      await new Promise((resolve) => setTimeout(resolve, speed));
    }
  };

  const generateJury = async () => {
    setIsGenerating(true);
    setJury([undefined, undefined, undefined, undefined]);

    const shuffled = [...professors].sort(() => Math.random() - 0.5);
    const selected = shuffled.slice(0, 4);

    await Promise.all(
      selected.map(
        (target, index) =>
          new Promise((resolve) => {
            setTimeout(() => {
              animateSlot(index, target).then(resolve);
            }, index * 2000); // Stagger start times for each slot by 2 seconds
          })
      )
    );

    // Save to localStorage after generation
    const history = JSON.parse(localStorage.getItem("juryHistory") || "[]");
    history.push({
      date: new Date().toISOString(),
      jury: selected,
    });
    localStorage.setItem("juryHistory", JSON.stringify(history));

    setIsGenerating(false);
  };

  return (
    <div className="bg-gradient-to-br from-indigo-100 via-blue-50 to-purple-100 flex items-center justify-center p-6">
      <div className="max-w-6xl w-full bg-white rounded-3xl shadow-2xl overflow-hidden">
        <div className="p-8 bg-gradient-to-r from-indigo-600 to-purple-600 text-white flex items-center justify-between">
          <div className="flex items-center space-x-4">
            <Users size={32} className="text-white" />
            <h1 className="text-3xl font-extrabold tracking-tight">
              Jury Generator
            </h1>
          </div>
          <button
            onClick={generateJury}
            disabled={isGenerating}
            className="flex items-center space-x-2 px-5 py-3 bg-white text-indigo-700 rounded-xl font-semibold hover:bg-indigo-50 transition-colors shadow-md disabled:opacity-50"
          >
            <RefreshCw
              size={20}
              className={isGenerating ? "animate-spin" : ""}
            />
            <span>{isGenerating ? "Shuffling..." : "Generate Jury"}</span>
          </button>
        </div>
        <div className="p-8 bg-gray-50/50">
          {jury.every((p) => p === undefined) && !isGenerating ? (
            <div className="text-center py-16 bg-white rounded-2xl shadow-inner">
              <p className="text-gray-600 text-xl font-medium">
                Press 'Generate Jury' to shuffle through professors and select 4
                randomly.
              </p>
              <p className="text-gray-400 mt-2">
                Watch the slow, visual shuffling animation for a realistic
                selection experience!
              </p>
            </div>
          ) : (
            <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-6">
              {jury.map((prof, index) => (
                <div
                  key={`slot-${index}`}
                  className="relative overflow-hidden rounded-2xl shadow-lg bg-white h-[380px]"
                >
                  <AnimatePresence mode="wait">
                    {prof && (
                      <motion.div
                        key={prof.id}
                        variants={slotVariants}
                        initial="enter"
                        animate="center"
                        exit="exit"
                        className="absolute w-full p-5 flex flex-col items-center"
                      >
                        <img
                          src={prof.photo}
                          alt={prof.fullName}
                          className="w-32 h-32 rounded-full object-cover mb-4 shadow-md"
                        />
                        <h3 className="text-lg font-bold text-gray-800 mb-1 text-center">
                          {prof.fullName}
                        </h3>
                        <p className="text-sm text-indigo-600 font-medium mb-2">
                          {prof.faculty}
                        </p>
                        <p className="text-sm text-gray-600 mb-4">
                          {prof.yearsOfWork} years
                        </p>
                        <div className="flex flex-wrap gap-2 justify-center">
                          {prof.specialties.slice(0, 3).map((spec, i) => (
                            <span
                              key={i}
                              className="px-3 py-1 text-xs rounded-full bg-indigo-100 text-indigo-700 font-medium"
                            >
                              {spec}
                            </span>
                          ))}
                        </div>
                      </motion.div>
                    )}
                  </AnimatePresence>
                  {!prof && (
                    <div className="absolute inset-0 flex items-center justify-center bg-gray-100">
                      <RefreshCw
                        size={48}
                        className="text-indigo-400 animate-spin"
                      />
                    </div>
                  )}
                </div>
              ))}
            </div>
          )}
        </div>
      </div>
    </div>
  );
}

const historyItemVariants = {
  hidden: { opacity: 0, y: 50 },
  visible: {
    opacity: 1,
    y: 0,
    transition: { duration: 0.5, ease: "easeOut" },
  },
};

const profCardVariants = {
  hidden: { opacity: 0, scale: 0.9 },
  visible: {
    opacity: 1,
    scale: 1,
    transition: { duration: 0.3, ease: "easeOut" },
  },
};

export function JuryHistory() {
  const [history, setHistory] = useState([]);

  useEffect(() => {
    const storedHistory = JSON.parse(
      localStorage.getItem("juryHistory") || "[]"
    );
    // Sort by date descending
    storedHistory.sort((a, b) => new Date(b.date) - new Date(a.date));
    setHistory(storedHistory);
  }, []);

  const formatDate = (isoString) => {
    const date = new Date(isoString);
    return date.toLocaleDateString("en-US", {
      year: "numeric",
      month: "long",
      day: "numeric",
    });
  };

  const formatTime = (isoString) => {
    const date = new Date(isoString);
    return date.toLocaleTimeString("en-US", {
      hour: "2-digit",
      minute: "2-digit",
      second: "2-digit",
      hour12: true,
    });
  };

  return (
    <div className="bg-gradient-to-br from-purple-100 via-indigo-50 to-blue-100 min-h-screen flex items-center justify-center p-6">
      <div className="max-w-7xl w-full bg-white rounded-3xl shadow-2xl overflow-hidden">
        <div className="p-8 bg-gradient-to-r from-purple-600 to-indigo-600 text-white flex items-center justify-between">
          <div className="flex items-center space-x-4">
            <Calendar size={32} className="text-white" />
            <h1 className="text-3xl font-extrabold tracking-tight">
              Jury History
            </h1>
          </div>
          <p className="text-sm font-medium opacity-80">
            {history.length} generations recorded
          </p>
        </div>
        <div className="p-8 bg-gray-50/50 max-h-[80vh] overflow-y-auto">
          {history.length === 0 ? (
            <div className="text-center py-16 bg-white rounded-2xl shadow-inner">
              <p className="text-gray-600 text-xl font-medium">
                No jury generations yet.
              </p>
              <p className="text-gray-400 mt-2">
                Generate a jury to start building your history!
              </p>
            </div>
          ) : (
            <div className="space-y-12">
              {history.map((entry, index) => (
                <motion.div
                  key={index}
                  variants={historyItemVariants}
                  initial="hidden"
                  animate="visible"
                  className="relative bg-white rounded-2xl shadow-lg p-6"
                >
                  <div className="absolute -top-4 left-1/2 -translate-x-1/2 bg-purple-600 text-white px-4 py-2 rounded-full shadow-md flex items-center space-x-2">
                    <Clock size={16} />
                    <span className="font-semibold">
                      {formatDate(entry.date)} at {formatTime(entry.date)}
                    </span>
                  </div>
                  <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-6 mt-6">
                    {entry.jury.map((prof, profIndex) => (
                      <motion.div
                        key={profIndex}
                        variants={profCardVariants}
                        initial="hidden"
                        animate="visible"
                        whileHover={{
                          scale: 1.05,
                          transition: { duration: 0.2 },
                        }}
                        className="bg-gradient-to-b from-gray-50 to-white rounded-xl shadow-md p-4 flex flex-col items-center hover:shadow-xl transition-shadow"
                      >
                        <img
                          src={prof.photo}
                          alt={prof.fullName}
                          className="w-24 h-24 rounded-full object-cover mb-3 shadow-sm"
                        />
                        <h3 className="text-md font-bold text-gray-800 mb-1 text-center">
                          {prof.fullName}
                        </h3>
                        <p className="text-xs text-purple-600 font-medium mb-1">
                          {prof.faculty}
                        </p>
                        <p className="text-xs text-gray-500 mb-3">
                          {prof.yearsOfWork} years experience
                        </p>
                        <div className="flex flex-wrap gap-1 justify-center">
                          {prof.specialties.slice(0, 3).map((spec, i) => (
                            <span
                              key={i}
                              className="px-2 py-1 text-xs rounded-full bg-purple-100 text-purple-700 font-medium"
                            >
                              {spec}
                            </span>
                          ))}
                        </div>
                      </motion.div>
                    ))}
                  </div>
                </motion.div>
              ))}
            </div>
          )}
        </div>
      </div>
    </div>
  );
}
