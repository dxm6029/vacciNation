DROP DATABASE IF EXISTS vaccination;
CREATE DATABASE vaccination;
USE vaccination;

CREATE TABLE insurance(
	insurance_id INT NOT NULL AUTO_INCREMENT,
    name VARCHAR(100),
    carrier VARCHAR(50) NOT NULL,
    group_number VARCHAR(50),
    member_id VARCHAR(50) NOT NULL,
    PRIMARY KEY (insurance_id)
);

CREATE TABLE state(
	state_code CHAR(2) NOT NULL,
    name VARCHAR(20) NOT NULL,
    PRIMARY KEY (state_code)
);

CREATE TABLE address(
    address_id INT NOT NULL AUTO_INCREMENT,
    zip CHAR(5) NOT NULL,
    street VARCHAR(50) NOT NULL,
    street_line2 VARCHAR(50),
    city VARCHAR(50) NOT NULL,
    state CHAR(2) NOT NULL,
    PRIMARY KEY (address_id),
    CONSTRAINT fk_location_state FOREIGN KEY (state) REFERENCES state (state_code) ON UPDATE CASCADE
);

CREATE TABLE citizen(
    citizen_id INT NOT NULL AUTO_INCREMENT,
    email VARCHAR(254),
    last_name VARCHAR(50) NOT NULL,
    first_name VARCHAR(50) NOT NULL,
    date_of_birth DATE NOT NULL,
    insurance_id INT,
    phone_number VARCHAR(15),
    address_id INT,
    id_type VARCHAR(20),
    id_number VARCHAR(50),
    PRIMARY KEY (citizen_id),
    CONSTRAINT fk_citizen_insurance FOREIGN KEY (insurance_id) REFERENCES insurance (insurance_id) ON UPDATE CASCADE,
    CONSTRAINT fk_citizen_address FOREIGN KEY (address_id) REFERENCES address (address_id) ON UPDATE CASCADE,
    CONSTRAINT citizen_contact CHECK (email IS NOT NULL OR phone_number IS NOT NULL)
);

CREATE TABLE staff(
	staff_id INT NOT NULL AUTO_INCREMENT,
    email VARCHAR(254) NOT NULL UNIQUE,
    username VARCHAR(50) NOT NULL UNIQUE,
    password CHAR(96) NOT NULL,
    last_name VARCHAR(50) NOT NULL,
    first_name VARCHAR(50) NOT NULL,
    token VARCHAR(255),
    PRIMARY KEY (staff_id)
);

CREATE TABLE role(
	role_id INT NOT NULL,
    name VARCHAR(50) NOT NULL,
    PRIMARY KEY (role_id)
);

CREATE TABLE staff_role(
	staff_id INT NOT NULL,
    role_id INT NOT NULL,
    granted_by INT NOT NULL,
    PRIMARY KEY (staff_id, role_id),
    CONSTRAINT fk_staff_role_staffid FOREIGN KEY (staff_id) REFERENCES staff (staff_id) ON UPDATE CASCADE,
    CONSTRAINT fk_staff_role_roleid FOREIGN KEY (role_id) REFERENCES role (role_id) ON UPDATE CASCADE,
    CONSTRAINT fk_staff_role_grantedby FOREIGN KEY (granted_by) REFERENCES staff (staff_id) ON UPDATE CASCADE
);

CREATE TABLE location(
	location_id INT NOT NULL AUTO_INCREMENT,
    name VARCHAR(50) NOT NULL,
    address_id INT NOT NULL,
    PRIMARY KEY (location_id),
    CONSTRAINT fk_location_address FOREIGN KEY (address_id) REFERENCES address (address_id) ON UPDATE CASCADE
);

CREATE TABLE staff_location(
	staff_id INT NOT NULL,
    location_id INT NOT NULL,
    PRIMARY KEY (staff_id, location_id),
    CONSTRAINT fk_staff_location_staff FOREIGN KEY (staff_id) REFERENCES staff (staff_id) ON UPDATE CASCADE,
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
    location_id INT NOT NULL,
    batch VARCHAR(100),
    PRIMARY KEY (dose_id),
    CONSTRAINT fk_dose_vaccine FOREIGN KEY (vaccine_id) REFERENCES vaccine (vaccine_id) ON UPDATE CASCADE,
    CONSTRAINT fk_dose_location FOREIGN KEY (location_id) REFERENCES location (location_id) ON UPDATE CASCADE
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
    status_id INT,
    reactions VARCHAR(255),
    PRIMARY KEY (timeslot_id),
    CONSTRAINT fk_timeslot_staff FOREIGN KEY (staff_id) REFERENCES staff (staff_id) ON UPDATE CASCADE,
    CONSTRAINT fk_timeslot_citizen FOREIGN KEY (citizen_id) REFERENCES citizen (citizen_id) ON UPDATE CASCADE,
    CONSTRAINT fk_timeslot_location FOREIGN KEY (location_id) REFERENCES location (location_id) ON UPDATE CASCADE,
    CONSTRAINT fk_timeslot_dose FOREIGN KEY (dose_id) REFERENCES dose (dose_id) ON UPDATE CASCADE,
    CONSTRAINT fk_timeslot_status FOREIGN KEY (status_id) REFERENCES timeslot_status (status_id) ON UPDATE CASCADE
);

CREATE TABLE eligibility(
	eligibility_id INT NOT NULL AUTO_INCREMENT,
    vaccine_id INT NOT NULL,
    dependency INT,
    PRIMARY KEY (eligibility_id),
    CONSTRAINT fk_eligibility_vaccine FOREIGN KEY (vaccine_id) REFERENCES vaccine (vaccine_id) ON UPDATE CASCADE    
);

CREATE TABLE eligibility_text(
	text_id INT NOT NULL,
    language CHAR(2) NOT NULL,
    eligibility_id INT NOT NULL,
    type CHAR(1) NOT NULL,
    text VARCHAR(1023) NOT NULL,
    is_eligible BOOL,
    PRIMARY KEY (text_id, language),
    CONSTRAINT type_is_qa CHECK (type IN ('Q', 'A')),
    CONSTRAINT only_a_eligible CHECK (type = 'A' OR is_eligible IS NULL),
    CONSTRAINT fk_eligibility_text FOREIGN KEY (eligibility_id) REFERENCES eligibility (eligibility_id) ON UPDATE CASCADE ON DELETE CASCADE
);

ALTER TABLE eligibility ADD CONSTRAINT fk_eligibility_dependency FOREIGN KEY (dependency) REFERENCES eligibility_text (text_id) ON UPDATE CASCADE ON DELETE CASCADE;

CREATE TABLE faq(
	faq_id INT NOT NULL,
    type CHAR(1) NOT NULL,
    language CHAR(2) NOT NULL,
    text VARCHAR(1023) NOT NULL,
    PRIMARY KEY (faq_id, type, language),
    CONSTRAINT faq_type_is_qa CHECK (type IN ('Q', 'A'))
);

CREATE TABLE vaccine_faq(
	vaccine_id INT NOT NULL,
    faq_id INT NOT NULL,
    PRIMARY KEY (vaccine_id, faq_id),
    CONSTRAINT fk_vaccine_faq_vaccine FOREIGN KEY (vaccine_id) REFERENCES vaccine (vaccine_id) ON UPDATE CASCADE,
    CONSTRAINT fk_vaccine_faq_faq FOREIGN KEY (faq_id) REFERENCES faq (faq_id) ON UPDATE CASCADE
);

CREATE TABLE change_history(
	change_id INT NOT NULL AUTO_INCREMENT,
	change_table VARCHAR(50) NOT NULL,
    change_row VARCHAR(50) NOT NULL,
    row_id INT NOT NULL,
    old_val VARCHAR(254) NOT NULL,
    new_val VARCHAR(254) NOT NULL,
    change_date DATETIME NOT NULL,
    changed_by INT NOT NULL,
    PRIMARY KEY (change_id),
    CONSTRAINT fk_history_staff FOREIGN KEY (changed_by) REFERENCES staff (staff_id) ON UPDATE CASCADE
);

CREATE TABLE nav_link(
	link_id INT NOT NULL,
    address VARCHAR(511),
    title_en VARCHAR(50),
    title_es VARCHAR(50),
    PRIMARY KEY(link_id)
);

CREATE TABLE content(
	content_id INT NOT NULL AUTO_INCREMENT,
    label VARCHAR(20),
    text_en TEXT,
    text_es TEXT,
    PRIMARY KEY (content_id)
);

CREATE TABLE monitor(
    monitor_id INT NOT NULL AUTO_INCREMENT,
    application VARCHAR(50) NOT NULL,
    endpoint VARCHAR(50) NOT NULL,
    response_time INT NOT NULL,
    response_code CHAR(3) NOT NULL,
    timestamp DATETIME NOT NULL,
    PRIMARY KEY (monitor_id)
);