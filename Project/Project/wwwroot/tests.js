
const baseUrl = 'api'; 
async function apiRequest(url, method = 'GET', body = null) {
    const options = {
        method: method,
        headers: {
            'Content-Type': 'application/json'
        }
    };

    if (body) {
        options.body = JSON.stringify(body);
    }

    try {
        const response = await fetch(url, options);
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        return await response.json();
    } catch (error) {
        console.error('API request failed:', error);
        throw error; 
    }
}

async function testStudentsApi() {
    console.log('--- Testing Students API ---');


    try {
        const students = await apiRequest(`${baseUrl}/students`);
        console.log('Get all students:', students);
    } catch (error) {
        console.error('Failed to get students:', error);
    }

    const newStudent = {
        firstName: 'John',
        lastName: 'Doe',
        dateOfBirth: '2000-01-01',
        email: 'john.doe@example.com',
        phone: '123-456-7890'
    };

    let createdStudent;
    try {
        createdStudent = await apiRequest(`${baseUrl}/students`, 'POST', newStudent);
        console.log('Created student:', createdStudent);
    } catch (error) {
        console.error('Failed to create student:', error);
    }

    let studentId = createdStudent.studentId;
    try {
        const student = await apiRequest(`${baseUrl}/students/${studentId}`);
        console.log('Get student by ID:', student);
    } catch (error) {
        console.error('Failed to get student by ID:', error);
    }

    try {
        const courses = await apiRequest(`${baseUrl}/students/${studentId}/courses`);
        console.log('Get courses for student:', courses);
    } catch (error) {
        console.error('Failed to get courses for student:', error);
    }
}

async function testCoursesApi() {
    console.log('--- Testing Courses API ---');

    const courses = await apiRequest(`${baseUrl}/courses`);
    console.log('Get all courses:', courses);

    const newCourse = {
        courseName: 'Introduction to JavaScript',
        description: 'A beginner-friendly course on JavaScript',
        credits: 3
    };
    const createdCourse = await apiRequest(`${baseUrl}/courses`, 'POST', newCourse);
    console.log('Created course:', createdCourse);

    const courseId = createdCourse.courseId;
    const course = await apiRequest(`${baseUrl}/courses/${courseId}`);
    console.log('Get course by ID:', course);

    const updatedCourse = {
        courseName: 'Advanced JavaScript',
        credits: 4
    };

    const students = await apiRequest(`${baseUrl}/courses/${courseId}/students`);
    console.log('Get students for course:', students);

    const teachers = await apiRequest(`${baseUrl}/courses/${courseId}/teachers`);
    console.log('Get teachers for course:', teachers);

}


async function testTeachersApi() {
    console.log('--- Testing Teachers API ---');

    const teachers = await apiRequest(`${baseUrl}/teachers`);
    console.log('Get all teachers:', teachers);

    const newTeacher = {
        firstName: 'Alice',
        lastName: 'Smith',
        email: 'alice.smith@example.com',
        phone: '987-654-3210'
    };
    const createdTeacher = await apiRequest(`${baseUrl}/teachers`, 'POST', newTeacher);
    console.log('Created teacher:', createdTeacher);

    const teacherId = createdTeacher.teacherId;
    const teacher = await apiRequest(`${baseUrl}/teachers/${teacherId}`);
    console.log('Get teacher by ID:', teacher);

    const updatedTeacher = {
        firstName: 'Alicia',
        email: 'alicia.smith@example.com'
    };
    const courses = await apiRequest(`${baseUrl}/teachers/${teacherId}/courses`);
    console.log('Get courses for teacher:', courses);

}
async function testCourseEnrollmentsApi() {
    console.log('--- Testing Course Enrollments API ---');

    const newEnrollment = {
        studentId: 1, 
        courseId: 1
    };
    const createdEnrollment = await apiRequest(`${baseUrl}/courseenrollments`, 'POST', newEnrollment);
    console.log('Created enrollment:', createdEnrollment);

    const enrollmentId = createdEnrollment.enrollmentId;
    const enrollment = await apiRequest(`${baseUrl}/courseenrollments/${enrollmentId}`);
    console.log('Get enrollment by ID:', enrollment);
}

async function testTeacherCoursesApi() {
    console.log('--- Testing Teacher Courses API ---');

    const newTeacherCourse = {
        teacherId: 1,
        courseId: 1
    };
    const createdTeacherCourse = await apiRequest(`${baseUrl}/teachercourses`, 'POST', newTeacherCourse);
    console.log('Created teacher-course association:', createdTeacherCourse);
}

async function runAllTests() {
    await testStudentsApi();
    await testCoursesApi();
    await testTeachersApi();
    await testCourseEnrollmentsApi();
    await testTeacherCoursesApi();
}

runAllTests();
