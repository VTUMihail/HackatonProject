/* eslint-disable no-unused-vars */
// services/campsiteService.js

import axios from "axios";
import { request } from "../utils/request";

export const getTeachers = async () => {
  try {
    const response = await request.get("/teachers/");

    return response.data;
  } catch (error) {
    console.log(error);
  }
};
export const getTeacherById = async (id) => {
  try {
    const response = await request.get(`/teachers/${id}`);
    return response.data;
  } catch (error) {
    console.log(error);
    throw error;
  }
};
