class Response {
  /**
   *
   */
  constructor() {
    this.isSuccess;
    this.data;
    this.message;
  }
  static fail(data = null, message = "") {
    const response = new Response();
    response.data = data;
    response.isSuccess = false;
    response.message = message;

    return response;
  }

  static success(data = null, message = "") {
    const response = new Response();
    response.data = data;
    response.isSuccess = true;
    response.message = message;

    return response;
  }
}

module.exports = { Response };
