DROP DATABASE IF EXISTS vaccination;
CREATE DATABASE vaccination;
USE vaccination;

CREATE TABLE insurance(
	insurance_id INT NOT NULL AUTO_INCREMENT,
    last_name VARCHAR(50) NOT NULL,
    first_name VARCHAR(50) NOT NULL,
    carrier VARCHAR(50) NOT NULL,
    group_number INT,
    member_id VARCHAR(50) NOT NULL,
    PRIMARY KEY (insurance_id)
);

CREATE TABLE user(
	user_id INT NOT NULL AUTO_INCREMENT,
    email VARCHAR(254),
    username VARCHAR(50),
    password CHAR(96),
    last_name VARCHAR(50) NOT NULL,
    first_name VARCHAR(50) NOT NULL,
    insurance_id INT,
    PRIMARY KEY (user_id),
    CONSTRAINT fk_user_insurance FOREIGN KEY (insurance_id) REFERENCES insurance (insurance_id) ON UPDATE CASCADE
);

CREATE TABLE role(
	role_id INT NOT NULL,
    name VARCHAR(50) NOT NULL,
    PRIMARY KEY (role_id)
);

CREATE TABLE user_role(
	user_id INT NOT NULL,
    role_id INT NOT NULL,
    granted_by INT NOT NULL,
    PRIMARY KEY (user_id, role_id),
    CONSTRAINT fk_user_role_userid FOREIGN KEY (user_id) REFERENCES user (user_id) ON UPDATE CASCADE,
    CONSTRAINT fk_user_role_roleid FOREIGN KEY (role_id) REFERENCES role (role_id) ON UPDATE CASCADE,
    CONSTRAINT fk_user_role_grantedby FOREIGN KEY (granted_by) REFERENCES user (user_id) ON UPDATE CASCADE
);

CREATE TABLE state(
	state_code CHAR(2) NOT NULL,
    name VARCHAR(13) NOT NULL,
    PRIMARY KEY (state_code)
);

CREATE TABLE location(
	location_id INT NOT NULL AUTO_INCREMENT,
    zip CHAR(5),
    street VARCHAR(50),
    city VARCHAR(50),
    state CHAR(2),
    PRIMARY KEY (location_id),
    CONSTRAINT fk_location_state FOREIGN KEY (state) REFERENCES state (state_code) ON UPDATE CASCADE
);

CREATE TABLE staff_location(
	staff_id INT NOT NULL,
    location_id INT NOT NULL,
    PRIMARY KEY (staff_id, location_id),
    CONSTRAINT fk_staff_location_staff FOREIGN KEY (staff_id) REFERENCES user (user_id) ON UPDATE CASCADE,
    CONSTRAINT fk_staff_location_location FOREIGN KEY (location_id) REFERENCES location (location_id) ON UPDATE CASCADE
);

CREATE TABLE vaccine_category(
	category_id INT NOT NULL,
    name VARCHAR(50) NOT NULL,
    PRIMARY KEY (category_id)
);

CREATE TABLE vaccine_disease(
	disease_id INT NOT NULL,
    name VARCHAR(50) NOT NULL,
    PRIMARY KEY (disease_id)
);

CREATE TABLE vaccine(
	vaccine_id INT NOT NULL AUTO_INCREMENT,
    category INT NOT NULL,
    disease INT NOT NULL,
    description VARCHAR(255),
    PRIMARY KEY (vaccine_id),
    CONSTRAINT fk_vaccine_category FOREIGN KEY (category) REFERENCES vaccine_category (category_id) ON UPDATE CASCADE,
    CONSTRAINT fk_vaccine_disease FOREIGN KEY (disease) REFERENCES vaccine_disease (disease_id) ON UPDATE CASCADE
);

CREATE TABLE dose(
	dose_id INT NOT NULL AUTO_INCREMENT,
    supplier VARCHAR(50) NOT NULL,
    vaccine_id INT NOT NULL,
    PRIMARY KEY (dose_id),
    CONSTRAINT fk_dose_vaccine FOREIGN KEY (vaccine_id) REFERENCES vaccine (vaccine_id) ON UPDATE CASCADE
);

CREATE TABLE timeslot_status(
	status_id INT NOT NULL,
    description VARCHAR(50) NOT NULL,
    PRIMARY KEY (status_id)
);

CREATE TABLE timeslot(
	timeslot_id INT NOT NULL AUTO_INCREMENT,
    staff_id INT,
    citizen_id INT,
    location_id INT,
    dose_id INT,
    date DATETIME,
    status INT,
    PRIMARY KEY (timeslot_id),
    CONSTRAINT fk_timeslot_user_staff FOREIGN KEY (staff_id) REFERENCES user (user_id) ON UPDATE CASCADE,
    CONSTRAINT fk_timeslot_user_citizen FOREIGN KEY (citizen_id) REFERENCES user (user_id) ON UPDATE CASCADE,
    CONSTRAINT fk_timeslot_location FOREIGN KEY (location_id) REFERENCES location (location_id) ON UPDATE CASCADE,
    CONSTRAINT fk_timeslot_dose FOREIGN KEY (dose_id) REFERENCES dose (dose_id) ON UPDATE CASCADE,
    CONSTRAINT fk_timeslot_status FOREIGN KEY (status) REFERENCES timeslot_status (status_id) ON UPDATE CASCADE
);