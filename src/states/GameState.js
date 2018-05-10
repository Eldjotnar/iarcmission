import RainbowText from 'objects/RainbowText';
import GroundRobot from 'objects/GroundRobot';
var bots;

class GameState extends Phaser.State {
	preload() {
		this.game.load.image('grid', 'assets/game_board.png');
		this.game.load.image('gRobot', 'assets/ground_robot.png');
	}
	create() {
		this.game.time.desiredFps = 30;
		this.game.physics.startSystem(Phaser.Physics.P2JS);

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

		var botCollisionGroup = this.game.physics.p2.createCollisionGroup();
		this.game.physics.p2.setImpactEvents(true);
		bots = this.game.add.group();
		//bots.enableBody = true;
		//this.game.physics.enable(bots, Phaser.Physics.ARCADE);
		//bots = this.game.add.physicsGroup(Phaser.Physics.ARCADE);

		//let test = new GroundRobot(this.game, scaleFactor, 0, center.x, center.y, 'gRobot');
		//console.log('my bum');
		//bots.create(new GroundRobot(this.game, scaleFactor, 0, center.x, center.y, 'gRobot'));
		var numElements = 5;
    var angle = 0;
		var radius = 50; // px TODO: adjust to proper size
    var step = (2*Math.PI) / numElements;
    for(var i = 0; i < numElements; i++) {
			//console.log("here");
      var x = center.x + radius * Math.cos(angle);
      var y = center.y + radius * Math.sin(angle);
			//console.log(angle * 180/Math.PI);
			let test = new GroundRobot(this.game, scaleFactor, angle* 180/Math.PI, x, y, 'gRobot');
			// var bot = bots.create(new GroundRobot(this.game, scaleFactor, angle* 180/Math.PI, x, y, 'gRobot'));
			// console.log(test);
			// bots.add(test);
			// bot.body.setCircle(10);
			// test.body.setCollisionGroup(botCollisionGroup);
			// test.body.collides(botCollisionGroup);
      angle += step;
    }



		//var test = this.add.sprite(center.x, center.y, 'gRobot');
		// let test = new GroundRobot(this.game, scaleFactor, -90, center.x, center.y, 'gRobot');
		//let text = new RainbowText(this.game, center.x, center.y, "- phaser -\nwith a sprinkle of\nES6 dust!");
	}
}

export default GameState;
