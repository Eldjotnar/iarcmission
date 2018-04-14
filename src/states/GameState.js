import RainbowText from 'objects/RainbowText';

class GameState extends Phaser.State {
	preload() {
		//this.game.load.path = 'assets/';
		//console.log("path: " + this.game.load.path);
		//this.game.load.path = 'assets/';
		this.game.load.image('grid', 'assets/game_board.png');

	}
	create() {
		// measure the size of the window
	  var ww = window.innerWidth * window.devicePixelRatio;
	  var wh = window.innerHeight * window.devicePixelRatio;

		let center = { x: this.game.world.centerX, y: this.game.world.centerY }

	  // add the background grid and scale to screen
	  //var backgroundGrid = this.add.sprite(ww/4,wh/4,'grid');
		var backgroundGrid = this.add.sprite(center.x, center.y, 'grid');
		backgroundGrid.anchor.x = 0.5;
		backgroundGrid.anchor.y = 0.5;

	  var scaleFactor = (ww/4)/backgroundGrid.height/1.3;
		backgroundGrid.height = wh * scaleFactor;
		backgroundGrid.width = wh * scaleFactor;
		//console.log(backgroundGrid.height);

	  // actually adjust the height/width of the grid
	  //backgroundGrid.displayHeight = wh * scaleFactor;
	  //backgroundGrid.displayWidth = wh * scaleFactor;
		//console.log("test");
		//let center = { x: this.game.world.centerX, y: this.game.world.centerY }
		//let text = new RainbowText(this.game, center.x, center.y, "- phaser -\nwith a sprinkle of\nES6 dust!");
		//text.anchor.set(0.5);
	}

}

export default GameState;
