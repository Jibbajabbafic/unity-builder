import path from 'path';

export default class Action {
  static get supportedPlatforms() {
    return ['linux'];
  }

  static get isRunningLocally() {
    return process.env.RUNNER_WORKSPACE === undefined;
  }

  static get isRunningFromSource() {
    return path.basename(__dirname) === 'model';
  }

  static get name() {
    return 'unity-builder';
  }

  static get rootFolder() {
    if (Action.isRunningFromSource) {
      return path.dirname(path.dirname(path.dirname(__filename)));
    }

    return path.dirname(path.dirname(__filename));
  }

  static get builderFolder() {
    return `${Action.rootFolder}/builder`;
  }

  static get dockerfile() {
    return `${Action.builderFolder}/Dockerfile`;
  }

  static get workspace() {
    return process.env.GITHUB_WORKSPACE;
  }

  static checkCompatibility() {
    const currentPlatform = process.platform;
    if (!Action.supportedPlatforms.includes(currentPlatform)) {
      throw new Error(`Currently ${currentPlatform}-platform is not supported`);
    }
  }
}
