drop database PPTify;
CREATE DATABASE IF NOT EXISTS PPTify;
USE PPTify;

-- User Credentials Table (Now primary)
CREATE TABLE user_credentials (
    user_id CHAR(36) PRIMARY KEY, -- PRIMARY KEY
    password_hash VARCHAR(255) NOT NULL,
    two_factor_secret VARCHAR(512),
    password_reset_token VARCHAR(255),
    reset_token_expiry TIMESTAMP,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);

-- Users Table (now secondary)
CREATE TABLE users (
    user_id CHAR(36) PRIMARY KEY,
    email VARCHAR(255) UNIQUE NOT NULL,
    full_name VARCHAR(255) NOT NULL,
    role ENUM('admin', 'user') DEFAULT 'user',
    profile_picture_url VARCHAR(255),
    phone_number VARCHAR(20),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    is_active BOOLEAN DEFAULT TRUE,
    last_login_at TIMESTAMP,
    is_email_verified BOOLEAN DEFAULT FALSE,
    FOREIGN KEY (user_id) REFERENCES user_credentials(user_id)
);

-- Presentations Table
CREATE TABLE presentations (
    presentation_id CHAR(36) PRIMARY KEY DEFAULT (UUID()),
    user_id CHAR(36) NOT NULL,
    file_url VARCHAR(255) NOT NULL,
    status ENUM('pending', 'in_progress', 'completed', 'failed') DEFAULT 'pending',
    version INT DEFAULT 1,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (user_id) REFERENCES user_credentials(user_id)
);

-- Tasks Table
CREATE TABLE tasks (
    task_id CHAR(36) PRIMARY KEY DEFAULT (UUID()),
    presentation_id CHAR(36) NOT NULL,
    task_status ENUM('queued', 'in_progress', 'completed', 'failed') DEFAULT 'queued',
    result LONGTEXT,
    started_at TIMESTAMP,
    completed_at TIMESTAMP,
    duration INT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (presentation_id) REFERENCES presentations(presentation_id)
);

-- Auth Tokens Table
CREATE TABLE auth_tokens (
    token_id CHAR(36) PRIMARY KEY DEFAULT (UUID()),
    user_id CHAR(36) NOT NULL,
    token VARCHAR(512) NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    expires_at TIMESTAMP NOT NULL,
    revoked BOOLEAN DEFAULT FALSE,
    FOREIGN KEY (user_id) REFERENCES user_credentials(user_id)
);

-- Audit Logs Table
CREATE TABLE audit_logs (
    log_id CHAR(36) PRIMARY KEY DEFAULT (UUID()),
    user_id CHAR(36) NOT NULL,
    action_type VARCHAR(255) NOT NULL,
    action_details TEXT,
    log_level ENUM('INFO', 'WARNING', 'ERROR') DEFAULT 'INFO',
    source VARCHAR(50),
    ip_address VARCHAR(50),
    user_agent VARCHAR(255),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (user_id) REFERENCES user_credentials(user_id)
);

-- User Preferences Table
CREATE TABLE user_preferences (
    preference_id CHAR(36) PRIMARY KEY DEFAULT (UUID()),
    user_id CHAR(36) NOT NULL,
    theme VARCHAR(50) DEFAULT 'light',
    language VARCHAR(50) DEFAULT 'en',
    FOREIGN KEY (user_id) REFERENCES user_credentials(user_id)
);

-- Presentation History Table
CREATE TABLE presentation_history (
    history_id CHAR(36) PRIMARY KEY DEFAULT (UUID()),
    presentation_id CHAR(36) NOT NULL,
    change_type ENUM('created', 'updated', 'deleted') NOT NULL,
    changed_by CHAR(36) NOT NULL,
    change_details TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (presentation_id) REFERENCES presentations(presentation_id),
    FOREIGN KEY (changed_by) REFERENCES user_credentials(user_id)
);

-- Indexes
CREATE INDEX idx_users_email ON users(email);
CREATE INDEX idx_presentations_user_id ON presentations(user_id);
CREATE INDEX idx_tasks_presentation_id ON tasks(presentation_id);
CREATE INDEX idx_auth_tokens_user_id ON auth_tokens(user_id);
CREATE INDEX idx_audit_logs_user_id ON audit_logs(user_id);
CREATE INDEX idx_auth_tokens_expires_at ON auth_tokens(expires_at);
CREATE INDEX idx_audit_logs_action_type ON audit_logs(action_type);
