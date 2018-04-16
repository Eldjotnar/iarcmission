import RainbowText from 'objects/RainbowText';
import GroundRobot from 'objects/GroundRobot';

class GameState extends Phaser.State {
	preload() {
		this.game.load.image('grid', 'assets/game_board.png');
		this.game.load.image('gRobot', 'assets/ground_robot.png');
	}
	create() {
		this.game.time.desiredFps = 30;
		this.game.physics.startSystem(Phaser.Physics.ARCADE);

		// measure the size of the window
	  var ww = window.innerWidth * window.devicePixelRatio;
	  var wh = window.innerHeight * window.devicePixelRatio;
		let center = { x: this.game.world.centerX, y: this.game.world.centerY }

	  // add the background grid and scale to screen
		var backgroundGrid = this.add.sprite(center.x, center.y, 'grid');
		backgroundGrid.anchor.x = 0.5;
		backgroundGrid.anchor.y = 0.5;

	  var scaleFactor = (ww/4)/backgroundGrid.height/1.3;
		backgroundGrid.height = wh * scaleFactor;
		backgroundGrid.width = wh * scaleFactor;

		//var test = this.add.sprite(center.x, center.y, 'gRobot');
		let test = new GroundRobot(this.game, scaleFactor, center.x, center.y, 'gRobot');
		//let text = new RainbowText(this.game, center.x, center.y, "- phaser -\nwith a sprinkle of\nES6 dust!");
	}

}

export default GameState;
