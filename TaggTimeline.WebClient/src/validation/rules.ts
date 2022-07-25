// You could infer there's no errors if errors.length === 0, but this feels more explicit
export interface ValidationResponse {
  failed: boolean;
  errors: string[];
}

/**
 * Validates whether or not a provided value meets a "required" critera
 * @param value The value to check if it exists
 * @returns Whether or not the value is deemed to exist
 */
// eslint-disable-next-line
export function required(value: any) {
  const errored: ValidationResponse = {
    failed: true,
    errors: ["Value is required"],
  };

  if (value === undefined || value === null) {
    return errored;
  }

  if (typeof value === "string" && value.length === 0) {
    return errored;
  }

  return {
    failed: false,
    errors: [],
  };
}

/**
 * Validates a password to be valid for the backend
 * @todo Potentially consider support for non-english characters
 * @param password The password to validate
 * @returns Whether or not the password failed, and the errors if so
 */
export function validatePassword(password: string): ValidationResponse {
  const errors: string[] = [];
  const specailCharacters = /[ `!@#$%^&*()_+\-=[\]{};':"\\|,.<>/?~]/;

  if (password.length < 6) {
    errors.push("Passwords must be at least 6 characters");
  }

  // Magical regex
  if (/^(.)\1+$/.test(password)) {
    errors.push("Passwords must use at least 1 different characters");
  }

  if (!specailCharacters.test(password)) {
    errors.push("Passwords must contain at least one special character");
  }

  if (!/\d/.test(password)) {
    errors.push("Passwords must contain at least one number");
  }

  if (!/[a-z]/.test(password)) {
    errors.push("Passwords must container at least one lower case letter");
  }

  if (!/[A-Z]/.test(password)) {
    errors.push("Passwords must container at least one lower case letter");
  }

  return {
    failed: !!errors.length,
    errors,
  };
}
